﻿namespace DavidsList.Controllers
{
    using DavidsList.Data;
    using System.Threading.Tasks;
    using DavidsList.Data.DbModels;
    using Microsoft.AspNetCore.Mvc;
    using DavidsList.Models.FormModels;
    using Microsoft.AspNetCore.Identity;
    using AspNetCoreHero.ToastNotification.Abstractions;
    using DavidsList.Services.Interfaces;

    public class UserController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly IAccountInteractor accountInteractor;
        private readonly INotyfService _notyf;


        public UserController(SignInManager<User> signInManager, INotyfService notyf, IAccountInteractor interactor)
        {
            this.signInManager = signInManager;
            _notyf = notyf;
            this.accountInteractor = interactor;
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var possibleErrors = accountInteractor.TryRegisteringUser(model).Result;
            if (possibleErrors != null)
            {
                foreach (var curError in possibleErrors)
                {
                    ModelState.AddModelError(curError.Key, curError.Value);
                }
                return View(model);
            }

            return RedirectToAction("Login", "User");
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginFormModel model)
        {
            if (model.Username == null || model.Password == null)
            {
                return View(model);
            }
            var possibleErrors = accountInteractor.TryLoggingUserIn(model).Result;
            if(possibleErrors != null)
            {
                foreach (var curError in possibleErrors)
                {
                    ModelState.AddModelError(curError.Key, curError.Value);
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            _notyf.Success("You Logged Out Successfully! Redirecting to home page...");
            return RedirectToAction("Index", "Home");
        }
      
    }
   
}
