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
    public class SubmissionController : Controller
    {
        private AppDbContext _context;
        private IRepository<Submission> _repo;
        private UserManager<Student> _userManager;
        public SubmissionController(IRepository<Submission> repository, AppDbContext context, UserManager<Student> userManager)
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
        public IActionResult ChangeEntity(int id, string submission_title, string submission_text)
        {
            using (_context)
            {
                var submission = _context.Submissions.Where(x => x.Id == id).FirstOrDefault();
                submission.Text = submission_text;
                submission.Title = submission_title;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddSubmission(string submission_title, string submission_text, string discp_id)
        {
            using (_context)
            {
                var submission = new Submission
                {
                    Title = submission_title,
                    Text = submission_text,
                    UserId = _userManager.GetUserId(User),
                    DisciplineId = Convert.ToInt32(discp_id)
                };
                _context.Submissions.Add(submission);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var mySubmissions = _context.Submissions.Where(x => x.UserId == _userManager.GetUserId(User)).ToList();
            return View(mySubmissions);
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
                    var submission = new Submission
                    {
                        Title = "Test_Entry",
                        Text = "Test_Entry",
                        UserId = _userManager.GetUserId(User),
                        DisciplineId = Convert.ToInt32(666)
                    };
                    _context.Submissions.Add(submission);
                    _context.SaveChanges();
                }
            }
        }
    }
}
