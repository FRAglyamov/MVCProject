using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCProject.Contexts;
using MVCProject.Services;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class LessonController : CrudController<Lesson>
    {
        private readonly AppDbContext _db;

        public LessonController(IRepository<Lesson> repository, AppDbContext context) : base(repository)
        {
            _db = context;
        }

        [HttpGet]
        public override IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public override IActionResult Edit(int id)
        {
            return View(Repository.Select(id));
        }

        [HttpGet]
        public override IActionResult Details(int id)
        {
            return View(Repository.Select(id));
        }
    }
}
