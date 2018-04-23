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

namespace MVCProject.Controllers
{
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
            ViewBag.Disciplines = _context.Disciplines.ToList();
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View(_repo.Select(id));
        }
    }
}
