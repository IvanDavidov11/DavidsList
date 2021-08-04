namespace DavidsList.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller

    {
        public IActionResult Result (string query)
        {
            return View();
        }


    }
}
