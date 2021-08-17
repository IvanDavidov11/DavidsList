namespace DavidsList.Controllers
{
    using DavidsList.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    public class MyMoviesController : Controller
    {
        private readonly IDatabaseInteractor dbInt;

        public MyMoviesController(IDatabaseInteractor dbInt)
        {
            this.dbInt = dbInt;
        }
        public IActionResult Favourites()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(dbInt.GetMarkedMovieViewModel_Favourited());
        }
        public IActionResult Seen()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(dbInt.GetMarkedMovieViewModel_Seen());
        }
        public IActionResult Flagged()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(dbInt.GetMarkedMovieViewModel_Flagged());
        }

    }
}
