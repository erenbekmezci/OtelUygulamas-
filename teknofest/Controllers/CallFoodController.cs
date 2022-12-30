using Microsoft.AspNetCore.Mvc;

namespace teknofest.Controllers
{
    public class CallFoodController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
