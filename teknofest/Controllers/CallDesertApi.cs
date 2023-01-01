using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace teknofest.Controllers
{
    public class CallDesertApi : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<FoodCategory> foodCategories = new List<FoodCategory>();
            var hhtc = new HttpClient();
            var response = await hhtc.GetAsync("https://localhost:7220/api/DesertApi");
            string resString = await response.Content.ReadAsStringAsync();
            foodCategories = JsonConvert.DeserializeObject<List<FoodCategory>>(resString);
            return View(foodCategories);
        }
    }
}
