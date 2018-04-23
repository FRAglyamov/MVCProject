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

namespace MVCProject.Controllers
{
    public class ReviewController : Controller
    {
        private AppDbContext _context;
        private IRepository<Work> _repo;
        private UserManager<Student> _userManager;
        public ReviewController(IRepository<Work> repository, AppDbContext context, UserManager<Student> userManager)
        {
            _repo = repository;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var otherWorks = _context.Works.Where(x => x.UserId != _userManager.GetUserId(User)).ToList();
            return View(otherWorks);
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

        /*public IActionResult AA(int id, string qinput_mark)
        {
            return Content(qinput_mark);
        }*/
        public IActionResult Check(int id, string input_mark)
        {
            //return Content(id.ToString() + input_mark);

            double mark;
            Double.TryParse(input_mark, NumberStyles.Any, CultureInfo.InvariantCulture, out mark);

            //double mark;
            //mark = Convert.ToDouble(input_mark);-
            //return Content((mark).ToString());

            using (_context)
            {
                var work = _context.Works.Where(x => x.Id == id).FirstOrDefault();
                /*if (work.FirstMarkUserId == _userManager.GetUserId(User) || work.SecondMarkUserId == _userManager.GetUserId(User) || work.ThirdMarkUserId == _userManager.GetUserId(User))
                    return RedirectToAction("Index");*/
                if (work.FirstMarkUserId == _userManager.GetUserId(User))
                { work.FirstMark = mark; goto m1; }
                else if (work.SecondMarkUserId == _userManager.GetUserId(User))
                { work.SecondMark = mark; goto m1; }
                else if (work.ThirdMarkUserId == _userManager.GetUserId(User))
                { work.ThirdMark = mark; goto m1; }
                if (work.FirstMark == 0)
                {
                    work.FirstMark = mark;
                    work.FirstMarkUserId = _userManager.GetUserId(User);
                    goto m1;
                }
                else if(work.SecondMark == 0)
                {
                    work.SecondMark = mark;
                    work.SecondMarkUserId = _userManager.GetUserId(User);
                    goto m1;
                }
                else if(work.ThirdMark == 0)
                {
                    work.ThirdMark = mark;
                    work.ThirdMarkUserId = _userManager.GetUserId(User);
                    goto m1;
                }
                m1:
                if (work.FirstMark!=0 && work.SecondMark!=0 && work.ThirdMark!=0)
                {
                    double first = Convert.ToDouble(work.FirstMark);
                    double second = Convert.ToDouble(work.SecondMark);
                    double third = Convert.ToDouble(work.ThirdMark);
                    work.FinalMark = first + second + third / 3.0;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
