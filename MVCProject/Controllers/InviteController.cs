using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Services;
using MVCProject.Contexts;
using Microsoft.AspNetCore.Identity;
using MVCProject.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace MVCProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class InviteController : Controller
    {

        private readonly UserManager<Student> _userManager;
        private AppDbContext _context;
        public InviteController(AppDbContext context, UserManager<Student> userManager)
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
        public async Task<IActionResult> SendInvite(string email)
        {            
            Student user = await _userManager.GetUserAsync(User);
            EmailService es = new EmailService();
            var host = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            string code = ServerCrypto.NetCrypto.GetHashString(RandomString(11));
            var invite = new Invite
            {
                Code = code
            };
            _context.Invites.Add(invite);
            _context.SaveChanges();
            await es.SendEmailAsync(email, $"Invite code: {code}");
            return RedirectToAction("Index");
        }
    }
}