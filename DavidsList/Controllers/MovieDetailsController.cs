namespace DavidsList.Controllers
{
    using DavidsList.Services;
    using Microsoft.AspNetCore.Mvc;

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
