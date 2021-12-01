using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Models;
using ServiceStack.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> usermanager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> user1, SignInManager<IdentityUser> user2,RoleManager<IdentityRole> user3)
        {
            usermanager = user1;
            signInManager = user2;
            roleManager = user3;
        }
    
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                var roleexsit = await roleManager.RoleExistsAsync("admin");
                if(!roleexsit)
                {
                    await roleManager.CreateAsync(new IdentityRole("admin"));
                }
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.UserName
                };

                var result = await usermanager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await usermanager.AddToRoleAsync(user,"admin");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index","Home");
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            return View(model);
        }
       
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string rurl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.rememberme, false);

                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("VoterId", model.UserName);

                    if (!string.IsNullOrEmpty(rurl))
                    {
                        return LocalRedirect(rurl);
                    }
                    else
                    {
                        return RedirectToAction("index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "invalid login attempt");
            }

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
