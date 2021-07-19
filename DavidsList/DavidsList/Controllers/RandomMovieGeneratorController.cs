using Microsoft.AspNetCore.Mvc;

namespace DavidsList.Controllers
{
    public class RandomMovieGeneratorController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Specific()
        {
            return View();
        }
    }
}
