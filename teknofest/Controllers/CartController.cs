using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using teknofest.Identity;
using teknofest.Models;

namespace teknofest.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;

        private IOrderService _orderService;

        private UserManager<User> _userManager;
        public CartController(IOrderService orderService, ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
            _orderService = orderService;
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
        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var orderModel = new OrderModel();

            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    FoodId = i.FoodId,
                    Name = i.Food.Name,
                    Price = i.Food.Price,
                    ImageUrl = i.Food.imageUrl,
                    Quantity = i.Quantity

                }).ToList()
            };

            return View(orderModel);
        }
        [HttpPost]
        public IActionResult Checkout(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var cart = _cartService.GetCartByUserId(userId);

                model.CartModel = new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemModel()
                    {
                        CartItemId = i.Id,
                        FoodId = i.FoodId,
                        Name = i.Food.Name,
                        Price = i.Food.Price,
                        ImageUrl = i.Food.imageUrl,
                        Quantity = i.Quantity
                    }).ToList()
                };

                

                SaveOrder(model, userId);
                ClearCart(model.CartModel.CartId);
                return View("Succes");
                
               
            }


            return View(model);
        }

        private void ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);
        }

        private void SaveOrder(OrderModel model, string userId)
        {
            var order = new Order();

            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrderState = EnumOrderState.tamamlandı;
            order.PaymentType = EnumPaymentType.CreditCard;


            order.OrderDate = new DateTime();
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.UserId = userId;
            
            order.Telefon = model.Telefon;
           

            order.OrderItems = new List<OrderItem>();

            foreach (var item in model.CartModel.CartItems)
            {
                var orderItem = new OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    FoodId = item.FoodId
                };
                order.OrderItems.Add(orderItem);
            }
            _orderService.Create(order);
        }


        public IActionResult GetOrders()
        {
            var userId = _userManager.GetUserId(User);
            var orders = _orderService.GetOrders(userId);

            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;
            foreach (var order in orders)
            {
                orderModel = new OrderListModel();

                orderModel.OrderId = order.Id;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.Telefon = order.Telefon;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                
                orderModel.OrderState = order.OrderState;
                orderModel.PaymentType = order.PaymentType;

                orderModel.OrderItems = order.OrderItems.Select(i => new OrderItemModel()
                {
                    OrderItemId = i.Id,
                    Name = i.Food.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ImageUrl = i.Food.imageUrl
                }).ToList();

                orderListModel.Add(orderModel);
            }


            return View("Orders", orderListModel);
        }

    }
}
