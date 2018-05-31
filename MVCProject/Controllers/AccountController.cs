using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MVCProject.Models;
using MVCProject.ViewModels;
using MVCProject.Services;
using MVCProject.Contexts;

namespace MVCProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Student> _userManager;
        private readonly SignInManager<Student> _signInManager;
        private AppDbContext _context;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext context, UserManager<Student> userManager, SignInManager<Student> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> EmailConfirmAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                EmailService es = new EmailService();
                var host = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                await es.SendEmailAsync(user.Email, $"{host}/Account/EmailConfirmSuccess?a={user.Id}&email={user.Email}");
                return RedirectToAction("EmailSendNotify", "Alert");
            }
            return RedirectToAction("EmailSuccess", "Alert");
        }
        public async Task<IActionResult> EmailConfirmSuccess(string a, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.EmailConfirmed = true;
                _context.SaveChanges();
            }
            return RedirectToAction("EmailSuccess", "Alert");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student user = new Student { Email = model.Email, UserName = model.Email, SecurityStamp = Guid.NewGuid().ToString() };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (model.InviteCode != null || model.InviteCode != "")
                {
                    //ищем инвайт с веденным кодом
                    var invite = _context.Invites.Where(x => x.Code == model.InviteCode).FirstOrDefault();
                    //если существует
                    if(invite != null)
                    {
                        //устанавливаем пользователю роль админа(макс права)
                        await _userManager.AddToRoleAsync(user, "admin");
                        //удаляем инвайт(т.к использован)
                        _context.Invites.Remove(invite);
                    }
                    else
                    {
                        goto m1;
                        return Content("Invite code broken!");
                    }
                }
                m1:
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
