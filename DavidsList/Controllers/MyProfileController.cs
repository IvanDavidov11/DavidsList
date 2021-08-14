namespace DavidsList.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using DavidsList.Data;
    using DavidsList.Data.DbModels;
    using DavidsList.Models.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    public class MyProfileController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly DavidsListDbContext data;
        private readonly INotyfService _notyf;


        public MyProfileController(UserManager<User> userManager, DavidsListDbContext db, INotyfService notyf)
        {
            this.userManager = userManager;
            this.data = db;
            _notyf = notyf;


        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home");
            }
            var curUser = data.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var model = new MyProfileViewModel
            {
                Email = curUser.Email,
                ImageUrl = curUser.ProfilePictureUrl == null ? "https://cdn.drawception.com/drawings/A4xPK14g50.png" : curUser.ProfilePictureUrl,
                Introduction = curUser.Introduction,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string intrd, string url)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home");
            }
            if (intrd != null)
            {
                data.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Introduction = intrd;
                _notyf.Success("Successfuly changed profile introdcution...");

            }
            if (url != null && Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                data.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).ProfilePictureUrl = url;
                _notyf.Success("Successfuly changed profile picture...");

            }
            else if (url != null)
            {
                ModelState.AddModelError("invalidUrl", "This Url is not valid. Please try another...");
            }
            data.SaveChanges();
            var curUser = data.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var model = new MyProfileViewModel
            {
                Email = curUser.Email,
                ImageUrl = curUser.ProfilePictureUrl == null ? "https://cdn.drawception.com/drawings/A4xPK14g50.png" : curUser.ProfilePictureUrl,
                Introduction = curUser.Introduction,
            };
            return View(model);
        }

    }
}
