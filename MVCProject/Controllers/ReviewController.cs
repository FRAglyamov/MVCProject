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
using System.Globalization;
using MVCProject.Filters;

namespace MVCProject.Controllers
{
    [EmailCheckFilter]
    public class ReviewController : Controller
    {
        private AppDbContext _context;
        private IRepository<Submission> _repo;
        private UserManager<Student> _userManager;
        public ReviewController(IRepository<Submission> repository, AppDbContext context, UserManager<Student> userManager)
        {
            _repo = repository;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var otherSubmissions = _context.Submissions.Where(x => x.UserId != _userManager.GetUserId(User)).ToList();
            return View(otherSubmissions);
        }
        public ActionResult GetJson()
        {
            var otherSubmissions = _context.Submissions.Where(x => x.UserId != _userManager.GetUserId(User)).ToList();
            return Json(otherSubmissions);
        }
        public IActionResult Create()
        {            
            return View();
        }

        [ReviewCheckFilter]
        public IActionResult Edit(int id)
        {
            var submission = _context.Submissions.Where(x => x.Id == id).FirstOrDefault();
            submission.isCheckedNow = true;
            _context.SaveChanges();
            return View(_repo.Select(id));
        }

        public IActionResult Check(int id, string input_mark)
        {

            double mark;
            Double.TryParse(input_mark, NumberStyles.Any, CultureInfo.InvariantCulture, out mark);

            using (_context)
            {
                var submission = _context.Submissions.Where(x => x.Id == id).FirstOrDefault();
                if (submission.FirstMarkUserId == _userManager.GetUserId(User))
                { submission.FirstMark = mark; goto m1; }
                else if (submission.SecondMarkUserId == _userManager.GetUserId(User))
                { submission.SecondMark = mark; goto m1; }
                else if (submission.ThirdMarkUserId == _userManager.GetUserId(User))
                { submission.ThirdMark = mark; goto m1; }
                if (submission.FirstMark == 0)
                {
                    submission.FirstMark = mark;
                    submission.FirstMarkUserId = _userManager.GetUserId(User);
                    goto m1;
                }
                else if(submission.SecondMark == 0)
                {
                    submission.SecondMark = mark;
                    submission.SecondMarkUserId = _userManager.GetUserId(User);
                    goto m1;
                }
                else if(submission.ThirdMark == 0)
                {
                    submission.ThirdMark = mark;
                    submission.ThirdMarkUserId = _userManager.GetUserId(User);
                    goto m1;
                }
                m1:
                if (submission.FirstMark!=0 && submission.SecondMark!=0 && submission.ThirdMark!=0)
                {
                    double first = Convert.ToDouble(submission.FirstMark);
                    double second = Convert.ToDouble(submission.SecondMark);
                    double third = Convert.ToDouble(submission.ThirdMark);
                    double finalMark = (first + second + third) / 3.0;                    
                    submission.FinalMark = String.Format("{0:F3}", finalMark);
                }
                submission.isCheckedNow = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
