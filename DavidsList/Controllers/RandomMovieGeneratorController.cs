using AspNetCoreHero.ToastNotification.Abstractions;
using DavidsList.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DavidsList.Controllers
{
    public class RandomMovieGeneratorController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IAccountInteractor accountInteractor;
        private readonly IGetInformationFromApi ApiConnector;
        public RandomMovieGeneratorController(IGetInformationFromApi connector, INotyfService notyf, IAccountInteractor accountInteractor)
        {
            this.ApiConnector = connector;
            this._notyf = notyf;
            this.accountInteractor = accountInteractor;
        }
        public IActionResult Index()
        {
            return View(accountInteractor.GetPreferencesModel_Specific());
        }
        public IActionResult Specific(int curGenre)
        {
            var moviePath = ApiConnector.GetRandomMoviePath_Specific(curGenre);
            return RedirectToAction("Index", "MovieDetails", new { id = moviePath });
        }
        public IActionResult Surprise()
        {
            var moviePath = ApiConnector.GetRandomMoviePath_Surprise();
            return RedirectToAction("Index", "MovieDetails", new { id = moviePath });
        }
        public IActionResult Preferred()
        {
            if (!User.Identity.IsAuthenticated)
            {
                _notyf.Error("You cannot access this feature without logging in...");
                return RedirectToAction("Index", "RandomMovieGenerator");
            }
            if (!ApiConnector.CheckIfUserHasFavouritedGenre())
            {
                _notyf.Error("You have no favourited genres. Go to \"MyProfile\" and then \"Prefrence\" to set them up...");
                return RedirectToAction("Index", "RandomMovieGenerator");
            }
            var moviePath = ApiConnector.GetRandomMoviePath_Preferred();
            return RedirectToAction("Index", "MovieDetails", new { id = moviePath });
        }
    }
}
