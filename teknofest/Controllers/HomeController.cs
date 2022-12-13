using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using teknofest.Models;

namespace teknofest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFoodCategoryDal service;

        public HomeController(ILogger<HomeController> logger , IFoodCategoryDal _service)
        {
            service = _service;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var foodCategories = new FoodCategoryList()
            {

                FoodCategories = service.GetAll()



            };
            return View(foodCategories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}