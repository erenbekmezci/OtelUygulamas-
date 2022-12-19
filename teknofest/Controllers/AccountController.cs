using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.webui.EmailServices;
using teknofest.Identity;
using teknofest.Models;

namespace teknofest.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager; //kullanıcı oluşturma falan login
        private SignInManager<User> _signInManager;//seession ve cookie
        private IEmailSender _emailSender;
        public AccountController(IEmailSender emailSender , UserManager<User> userManager , SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
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

            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen mail hesabınızı onaylayın!");
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
                //password usermanager ile alcaz çünkü hashlenecek şifrelenecek
            };

            var result = await _userManager.CreateAsync(user , model.Password);

            if (result.Succeeded) //kullanıcı oluşturma başaılı ise
            {
                //token ve email onaylama
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = token });
                //email 
                await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız", $"lütfen email hesabınızı onaylamak için linke <a href='https://localhost:7220{url}' >tıklayınız.</a>");

                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "bilinmeyen bir hata oluştu tekrar deneyiniz.");
            return View();
         
        }

        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            if (userid == null || token == null)
            {
                TempData["message"] = "geçersiz token";
                return View();
            }




            var user = await _userManager.FindByIdAsync(userid);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {

                    TempData["message"] = "Hesabınız onaylandı";
                    return View();
                }
            }
            TempData["message"] = "Hesabınız onaylanmadı";
            return View();


        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if(Email == null)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if(user == null)
                return View();

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
           

            var url = Url.Action("ResetPassword", "Account", new
            {
                userid = user.Id,
                token = code
            });

            await _emailSender.SendEmailAsync(Email, "Parolanızı Resetleyin", $"lütfen hesabınızın şifresini sıfırlamak için linke <a href='https://localhost:7220{url}' >tıklayınız.</a>");
            TempData["message"] = "Email hesabınıza link gönderildi";
            return View();
         
        }

        public IActionResult ResetPassword(string userid , string token)
        {
            if (userid == null || token == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var model = new ResetPasswordModel { Token = token };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(model.Email);


            if(user == null)
            {
                ModelState.AddModelError("", "Bu e - posta adresine ait kullanıcı bulunamadı!");
                return View();
            }

            await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            ModelState.AddModelError("", "Şifre sıfırlama başarılı !");
            return RedirectToAction("Login");


        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}
