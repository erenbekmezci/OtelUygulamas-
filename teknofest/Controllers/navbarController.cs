using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using teknofest.Identity;
using teknofest.Models;

namespace teknofest.Controllers
{
    public class navbarController : Controller
    {
        //ApplicationContext cnt = new ApplicationContext();
        /*
        public IActionResult Index()
        {
            var degerler = cnt.HakkimizdaSinifis.ToList();
            return View(degerler);
        }
        */
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
