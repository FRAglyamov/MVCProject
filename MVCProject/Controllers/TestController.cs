using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Contexts;
using MVCProject.Services;
using MVCProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace MVCProject.Controllers
{
    public class TestController : Controller
    {
        private IRepository<Work> _repo;
        private AppDbContext _dbCtx;
        private UserManager<Student> _userManager;
        public TestController(IRepository<Work> repo, AppDbContext dbCtx, UserManager<Student> userManager)
        {
            _repo = repo;
            _dbCtx = dbCtx;
            _userManager = userManager;
        }

        public async Task<IActionResult> TikTakAsync()
        {
            var user = await CheckUser();
            if (user != null)
                return Content(user.Email);
            else return Content("Not exist!");
        }

        public async Task<Student> CheckUser()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                return user;
            }
            else
                return null;
        }
        public IActionResult Index()
        {
            return View(_repo.Select().ToList());
        }
        public IActionResult Edit(int id)
        {
            return View(_repo.Select(id));
        }

        public void Save()
        {
            using(_dbCtx)
            _dbCtx.SaveChanges();
        }
    }
}