using Business.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using teknofest.Identity;
using teknofest.Models;

namespace teknofest.Controllers
{
    public class CartController : Controller
    {
        private ICartService _cartService;

        private UserManager<User> _userManager;
        public CartController(ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    FoodId = i.Food.Id,
                    Name = i.Food.Name,
                    Price = i.Food.Price,
                    ImageUrl = i.Food.imageUrl,
                    Quantity = i.Quantity

                }).ToList()
            });
        }
        [HttpPost]
        public IActionResult AddToCart(int foodid, int quantity)
        {
            _cartService.AddToCart(_userManager.GetUserId(User), foodid, quantity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int foodid)
        {
            _cartService.DeleteFromCart(_userManager.GetUserId(User), foodid);

            return RedirectToAction("Index");
        }
    }
}
