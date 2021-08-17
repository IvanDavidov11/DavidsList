namespace DavidsList.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using DavidsList.Data;
    using DavidsList.Data.DbModels;
    using DavidsList.Models.ViewModels;
    using DavidsList.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    public class MyProfileController : Controller
    {
        private readonly IAccountInteractor accountInteractor;
        public MyProfileController(IAccountInteractor interactor)
        {
            this.accountInteractor = interactor;
        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(accountInteractor.GetMyProfileViewModel());
        }
        [HttpPost]
        public IActionResult Index(string intrd, string url)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home");
            }
            if (url != null)
            {
                ModelState.AddModelError("invalidUrl", "This Url is not valid. Please try another...");
            }
            return View(accountInteractor.SetMyProfileDetails(intrd,url));
        }
        public IActionResult Preferences()
        {
            return View(accountInteractor.GetPreferencesModel());
        }
    }
}
