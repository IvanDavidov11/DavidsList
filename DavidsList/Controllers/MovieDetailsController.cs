namespace DavidsList.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using DavidsList.Services.Interfaces;

    public class MovieDetailsController : Controller
    {
        private readonly IGetInformationFromApi ApiConnector;

        public MovieDetailsController(IGetInformationFromApi connector)
        {
            this.ApiConnector = connector;
        }
        public IActionResult Index(string id)
        {
            var model =  this.ApiConnector.GetSpecificMovieDetails(id).Result;
            return View(model);
        }
    }
}
