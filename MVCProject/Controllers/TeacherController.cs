using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Contexts;
using Microsoft.AspNetCore.Identity;
using MVCProject.Filters;

namespace MVCProject.Controllers
{
    [TaskDateCheckFilter]
    public class TeacherController : Controller
    {
        private AppDbContext _context;
        private UserManager<Student> _userManager;
        public TeacherController(AppDbContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            Student teacher = await _userManager.GetUserAsync(User);
            string teacherId = teacher.Id;
            return View(_context.TeacherTasks.Where(x => x.TeacherId == teacherId).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Check()
        {
            return Content(DateTime.Now.Date.ToString());
        }
        //конвертация даты в правильный формат
        public string Force(string task_date)
        {
            var arr = task_date.Split('-');
            int year = Convert.ToInt32(arr[0]);
            int month = Convert.ToInt32(arr[1]);
            int day = Convert.ToInt32(arr[2]);
            var date = string.Format($"{day}.{month}.{year}");
            return date;
        }
        public async Task<IActionResult> AddTask(string task_title, string task_text, string task_date)
        {
            //return Content(task_date);
            Student teacher = await _userManager.GetUserAsync(User);
            string teacherId = teacher.Id;
            //return Content(task_title + "=" + task_text);
            var arr = task_date.Split('-');
            int year = Convert.ToInt32(arr[0]);
            int month = Convert.ToInt32(arr[1]);
            int day = Convert.ToInt32(arr[2]);
            var date = string.Format($"{day}.{month}.{year}");
            TeacherTask task = new TeacherTask
            {
                Title = task_title,
                Text = task_text,
                TeacherId = teacherId,
                Deadline = date,
                UnNormilizeDeadline = task_date
            };
            _context.TeacherTasks.Add(task);
            _context.SaveChanges();
            return RedirectToAction("Index", "Teacher");
        }
        public IActionResult Edit(int id)
        {
            TeacherTask task = _context.TeacherTasks.Where(x => x.Id == id).FirstOrDefault();
            return View(task);
        }
        public IActionResult EditTask(int id, string task_title, string task_text, string task_date)
        {
            TeacherTask task = _context.TeacherTasks.Where(x => x.Id == id).FirstOrDefault();
            task.Text = task_text;
            task.Title = task_title;
            task.Deadline = Force(task_date);
            task.UnNormilizeDeadline = task_date;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}