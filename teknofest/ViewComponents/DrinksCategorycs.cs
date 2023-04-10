using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace teknofest.ViewComponents
{
    public class DrinksCategorycs : ViewComponent
    {

        private IFoodCategoryDal _service;

        public DrinksCategorycs(IFoodCategoryDal service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
          
            return View(_service.GetAll(i=> i.Name.Contains("içecek") || i.Name.Contains("Içecek") || i.Name.Contains("icecek") || i.Name.Contains("İçecek")));
        }
    }
}
