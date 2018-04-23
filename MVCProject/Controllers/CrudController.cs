using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Services;

namespace MVCProject.Controllers
{
    public abstract class CrudController<T> : Controller
    {
        protected readonly IRepository<T> Repository;


        protected CrudController(IRepository<T> repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public virtual IActionResult Index()
        {
            return View(Repository.Select().ToList());
        }

        [HttpGet]
        public virtual IActionResult Details(int id)
        {
            return View(Repository.Select(id));
        }

        [HttpGet]
        public virtual IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Create(T item)
        {
            if (ModelState.IsValid)
            {
                Repository.Insert(item);
                Repository.Save();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual IActionResult Edit(int id)
        {
            return View(Repository.Select(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult EditS(T item)
        {
            Repository.Update(item);
            Repository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual IActionResult Delete(int id)
        {
            Repository.Delete(Repository.Select(id));
            Repository.Save();
            return RedirectToAction("Index");
        }
    }
}
