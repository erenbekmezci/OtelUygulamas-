using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;
using teknofest.Models;

namespace teknofest.Controllers
{
    public class MenuController : Controller
    {
        private IFoodCategoryDal _service;
        public MenuController(IFoodCategoryDal service)
        {
            _service = service;
        }
        public IActionResult List()
        {
            var foodCategories = new FoodCategoryList()
            {

                FoodCategories = _service.GetAll()



            };
            return View(foodCategories);
        }

        public IActionResult YemekMenu()
        {
            return View(_service.getYemekMenu());
        }
        public IActionResult IcecekMenu()
        {
            return View(_service.GetAll(i=> i.Url.Contains("icecek")));
        }
        

        public IActionResult Details(string url)
        {
            return View(_service.GetFoodsTheCategory(url));
        }
    }
}
