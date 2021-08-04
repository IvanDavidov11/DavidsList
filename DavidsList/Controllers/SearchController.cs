namespace DavidsList.Controllers
{
    using DavidsList.Services;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        private readonly IGetInformationFromApi ApiConnector;

        public SearchController(IGetInformationFromApi connector)
        {
            this.ApiConnector = connector;
        }
        [HttpGet]
        public IActionResult Result (string query)
        {
            var model = this.ApiConnector.GetSearchResultModel(query).Result;
            return View(model);
        }
    }
}
