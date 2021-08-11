namespace DavidsList.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using DavidsList.Services.Interfaces;

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
