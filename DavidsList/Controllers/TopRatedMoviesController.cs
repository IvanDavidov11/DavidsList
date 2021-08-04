namespace DavidsList.Controllers
{
    using DavidsList.Services;
    using Microsoft.AspNetCore.Mvc;

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
