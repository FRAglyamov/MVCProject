using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCProject.Contexts;
using MVCProject.Services;
using MVCProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using MVCProject.Filters;

namespace MVCProject.Controllers
{
    [EmailCheckFilter]
    public class WorkController : Controller
    {
        private AppDbContext _context;
        private IRepository<Work> _repo;
        private UserManager<Student> _userManager;
        public WorkController(IRepository<Work> repository, AppDbContext context, UserManager<Student> userManager)
        {
            _repo = repository;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> SetClaimAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Content("FUCK");
            var claims = _userManager.AddClaimAsync(user, new Claim("IsEmailConfirmed", "true"));
            return Content("Not FUCK");
        }
        public IActionResult GetClaim()
        {
            var val = User.Claims.FirstOrDefault()?.Value;
            return Content(val);
        }
        public IActionResult ChangeEntity(int id, string work_title, string work_text)
        {
            using (_context)
            {
                var work = _context.Works.Where(x => x.Id == id).FirstOrDefault();
                work.Text = work_text;
                work.Title = work_title;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddWork(string work_title, string work_text, string discp_id)
        {
            using (_context)
            {
                var work = new Work
                {
                    Title = work_title,
                    Text = work_text,
                    UserId = _userManager.GetUserId(User),
                    DisciplineId = Convert.ToInt32(discp_id)
                };
                _context.Works.Add(work);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var myWorks = _context.Works.Where(x => x.UserId == _userManager.GetUserId(User)).ToList();
            return View(myWorks);
        }
        public IActionResult Create()
        {
            ViewBag.Tasks = _context.TeacherTasks.ToList();
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View(_repo.Select(id));
        }

        public void TestAddEntity()
        {
            using (_context)
            {
                using (_context)
                {
                    var work = new Work
                    {
                        Title = "Test_Entry",
                        Text = "Test_Entry",
                        UserId = _userManager.GetUserId(User),
                        DisciplineId = Convert.ToInt32(666)
                    };
                    _context.Works.Add(work);
                    _context.SaveChanges();
                }
            }
        }
    }
}
