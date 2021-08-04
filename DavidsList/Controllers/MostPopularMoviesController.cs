namespace DavidsList.Controllers
{
    using DavidsList.Services;
    using Microsoft.AspNetCore.Mvc;

    public class MostPopularMoviesController : Controller
    {
        private readonly IGetInformationFromApi ApiConnector;

        public MostPopularMoviesController(IGetInformationFromApi connector)
        {
            this.ApiConnector = connector;
        }
        public IActionResult Index()
        {
            var model = this.ApiConnector.GetMoviesInParallel_MostPopular().Result;
            return View(model);
        }
    }
}
