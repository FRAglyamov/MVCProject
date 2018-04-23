using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCProject.Contexts;
using MVCProject.Services;
using MVCProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class StudentController : CrudController<Student>
    {
        protected StudentController(IRepository<Student> repository) : base(repository)
        {
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
