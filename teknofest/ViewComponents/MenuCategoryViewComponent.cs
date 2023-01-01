using DataAccess.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace teknofest.ViewComponents
{
    public class MenuCategoryViewComponent : ViewComponent
    {
        private IFoodCategoryDal _service;

        public MenuCategoryViewComponent(IFoodCategoryDal service)
        {
            _service = service; 
        }

        public IViewComponentResult Invoke()
        {
            //if (RouteData.Values["url"] != null)
            //    ViewBag.SelectedCategory = RouteData.Values["url"];
            return View(_service.getYemekMenu());
        }
    }
}
