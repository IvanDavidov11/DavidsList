namespace DavidsList.Controllers
{
    using DavidsList.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
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
        [IgnoreAntiforgeryToken]
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
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(accountInteractor.GetPreferencesModel());
        }
    }
}
