using Business.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using teknofest.Identity;
using teknofest.Models;

namespace teknofest.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager; //kullanıcı oluşturma falan login
        private SignInManager<User> _signInManager;//seession ve cookie
       

        private ICartService _cartService;
        public AccountController(UserManager<User> userManager , SignInManager<User> signInManager , ICartService cartService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        
            _cartService = cartService;
        }
        public IActionResult Login()
        {
            

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı bulunamadı !");
                return View();
            }

            
            

            var result = await _signInManager.PasswordSignInAsync(user , model.Password , true ,false);
            if(result.Succeeded)
            {
                return Redirect("~/"); //boşluksa anasayfaya git
            }
            ModelState.AddModelError("", "Bu kullanıcı adı veya parola yanlış !");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                OdaNumarasi = model.OdaNumarasi
                //password usermanager ile alcaz çünkü hashlenecek şifrelenecek
            };

            var result = await _userManager.CreateAsync(user , model.Password);

            if (result.Succeeded) //kullanıcı oluşturma başaılı ise
            {
                
                //kullanıcıya sepet oluşturma
                _cartService.InitializeCart(user.Id);

                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "bilinmeyen bir hata oluştu tekrar deneyiniz.");
            return View();
         
        }

        


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
 
            return Redirect("~/");
        }
    }
}
