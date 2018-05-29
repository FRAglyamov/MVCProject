using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerCrypto;
using MVCProject.Models;
using MVCProject.Services;
using Microsoft.AspNetCore.Identity;
using MVCProject.Contexts;
using System.Web;

namespace MVCProject.Controllers
{
    public class PasswordRecoveryController : Controller
    {
        private UserManager<Student> _userManager;
        private AppDbContext _context;
        public PasswordRecoveryController(AppDbContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        private static Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public async Task<IActionResult> Send(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Content("User not exist!");

            string magicWord = "lucklessfox";
            string hash = RandomString(6);
            using (_context)
            {
                RecoveryHash rhash = new RecoveryHash
                {
                    Hash = hash,
                    Magic = magicWord,
                    UserId = user.Id
                };
                _context.RecoveryHash.Add(rhash);
                _context.SaveChanges();
            }
            EmailService es = new EmailService();
            var host = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            await es.SendEmailAsync(user.Email, $"{host}/PasswordRecovery/RecoverAsync?a={user.Id}&data={hash}");
            return Content("RP success!");
        }
        public async Task<IActionResult> RecoverAsync(string a, string data)
        {
            var user = await _userManager.FindByIdAsync(a);
            if(user == null) return Content("Not Exist");
            var hashes = _context.RecoveryHash.Where(x => x.UserId == user.Id).ToList();

            if (hashes.Count() != 0)
            {
                return RedirectToAction("ChangePassword", "PasswordRecovery", new { user = user.Id });
            }
            else return Content("fff");
        }
        //вызов view изменения пароля 
        public IActionResult ChangePassword(string user)
        {
            ViewBag.UserId = user;
            return View();
        }
        //изменение пароля
        public async Task<IActionResult> PermitChanges(string new_pass, string user_id)
        {
            Student user = await _userManager.FindByIdAsync(user_id);
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, new_pass);           
            return RedirectToAction("PassChangeSuccess", "Alert");
        }
    }
}