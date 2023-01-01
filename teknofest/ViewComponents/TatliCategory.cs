using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace teknofest.ViewComponents
{
    public class TatliCategory : ViewComponent
    {
        private IFoodCategoryDal _service;

        public TatliCategory(IFoodCategoryDal service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {

            return View(_service.GetAll(i => i.Name.Contains("Tatlı") || i.Name.Contains("Tatli")));
        }
    }
}
