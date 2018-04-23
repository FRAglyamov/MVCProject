//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Contexts;
//using WebApplication1.Models;
//using Microsoft.AspNetCore.Identity;
//using System.Security.Claims;

//namespace WebApplication1.Controllers
//{
//    public class SubmissionController : Controller
//    {
//        private AppDbContext _context;
//        private readonly UserManager<Student> _userManager;
//        public SubmissionController(AppDbContext context, UserManager<Student> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }
//        public IActionResult Index()
//        {
//            if (_userManager.GetUserId(User) == null)
//                ViewBag.UserId = "null";
//            else
//                ViewBag.UserId = _userManager.GetUserId(User);
//            var userSubs = _context.Submissions.Where(x => x.UserId == _userManager.GetUserId(User)).ToList();
//            return View(userSubs);
//        }
//        public IActionResult CreateSubmission()
//        {
//            return View();
//        }
//        public IActionResult AddSubmission(string sub_title)
//        {
//            using (_context)
//            {
//                var sub = new Submission
//                {
//                    Title = sub_title,
//                    UserId = _userManager.GetUserId(User)
//                };
//                _context.Submissions.Add(sub);
//                _context.SaveChanges();
//            }
//            return RedirectToAction("Index");
//        }
//    }
//}