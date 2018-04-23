using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MVCProject.Models;
using MVCProject.ViewModels;

namespace MVCProject.Controllers
{
    public class UsersController : Controller
    {
        UserManager<Student> _userManager;

        public UsersController(UserManager<Student> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                Student student = new Student { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(student, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(string id)
        {
            Student student = await _userManager.FindByIdAsync(id);
            if (student == null)
                return NotFound();
            EditUserViewModel model = new EditUserViewModel { Id = student.Id, Email = student.Email };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student student = await _userManager.FindByIdAsync(model.Id);
                if (student != null)
                {
                    student.Email = model.Email;
                    student.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(student);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            Student student = await _userManager.FindByIdAsync(id);
            if (student != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(student);
            }
            return RedirectToAction("Index");
        }

    }
}
