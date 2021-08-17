using AspNetCoreHero.ToastNotification.Abstractions;
using DavidsList.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DavidsList.Controllers
{
    public class RandomMovieGeneratorController : Controller
    {
        private readonly INotyfService _notyf;

        private readonly IGetInformationFromApi ApiConnector;
        public RandomMovieGeneratorController(IGetInformationFromApi connector, INotyfService notyf)
        {
            this.ApiConnector = connector;
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Specific()
        {
            var moviePath = ApiConnector.GetRandomMoviePath_Specific();
            return RedirectToAction("Index", "MovieDetails", new { id = moviePath });
        }
        public IActionResult Surprise()
        {
            var moviePath = ApiConnector.GetRandomMoviePath_Specific();
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
