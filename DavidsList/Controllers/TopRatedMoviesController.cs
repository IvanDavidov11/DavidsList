namespace DavidsList.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using DavidsList.Services.Interfaces;

    public class TopRatedMoviesController : Controller
    {
        private readonly IGetInformationFromApi ApiConnector;

        public TopRatedMoviesController(IGetInformationFromApi connector)
        {
            this.ApiConnector = connector;
        }
        public IActionResult Index()
        {
            var model = this.ApiConnector.GetMoviesInParallel_MostRated().Result;
            return View(model);
        }
    }
}
