using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class AlertController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EmailNotConfirmed()
        {
            return View();
        }
        public IActionResult EmailSendNotify()
        {
            return View();
        }
        public IActionResult EmailSuccess()
        {
            return View();
        }
        public IActionResult PassChangeSuccess()
        {
            return View();
        }
        public IActionResult TaskEnd()
        {
            return View();
        }
    }
}