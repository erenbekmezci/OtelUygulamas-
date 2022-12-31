using DataAccess.Abstract;
using DataAccess.Concrete.EfCore;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Xml;
using teknofest.Identity;
using teknofest.Models;

namespace teknofest.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IFoodDal _foodService;
        private IFoodCategoryDal _foodCategoryService;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;

        public AdminController(IFoodDal foodService, IFoodCategoryDal foodCategoryService , RoleManager<IdentityRole> roleManager , UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _foodCategoryService = foodCategoryService;
            _foodService = foodService;
        }

        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            if(ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Name));
                return RedirectToAction("RoleList");
            }
            return View();
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonmembers = new List<User>();

            var user =  _userManager.Users.ToList();

            foreach (var _user in user)
            {
                var list =  await _userManager.IsInRoleAsync(_user, role.Name) ? members : nonmembers;
                //list = members liste membersi gösterecek işret edecek ekliyeceği yer members yani aynı adres
                list.Add(_user); //members.add(user);
            }

            var roleDetails = new RoleDetails()
            {
                Role = role,
                Members = members,
                NoMembers = nonmembers
            };
            return View(roleDetails);



        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditModel model)
        {
            if(ModelState.IsValid)
            {
                foreach (var userid in model.IdsToAdd ?? new string[]{ })
                {
                    var user = await _userManager.FindByIdAsync(userid);
                    if(user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if(!result.Succeeded)
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }


                }
                foreach (var userid in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userid);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }


                }
            }
            return RedirectToAction("EditRole");
        }

        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.roles2 = _roleManager.Roles.Select(i=> i.Name);
            if (user != null)
            {
                return View(new UserDetailModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = (List<string>)roles
                });
                
                

            }
            return View("UserList");

        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserDetailModel model , string[] selectedRoles)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync (model.UserId);

                if(user != null)
                {
                    user.UserName = model.UserName;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;
                    
                    var result = await _userManager.UpdateAsync(user);

                    if(result.Succeeded)
                    {
                        List<string>? userRoles = await _userManager.GetRolesAsync(user) as List<string>;
                        
                        selectedRoles = selectedRoles ?? new string[] { };

                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());
                        return RedirectToAction("UserList");


                    }
                   
                }
                var roles1 = _roleManager.Roles.Select(i => i.Name);
                ViewBag.roles2 = roles1;
                return View(model);

            }
            var roles = _roleManager.Roles.Select(i => i.Name);
            ViewBag.roles2 = roles;
            return View(model);

        }


        public IActionResult FoodList()
        {

            return View(_foodService.GetAll());
        }

        public IActionResult FoodCategoryList()
        {

            return View(_foodCategoryService.GetAll());
        }

        [HttpGet]
        public IActionResult CreateFood()
        {

            ViewBag.FoodCategoryIds = new SelectList(_foodCategoryService.GetAll(), "FoodCategoryId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult CreateFood(FoodModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Food()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    FoodCategoryId = model.FoodCategoryId,
                    isApproved = model.isApproved,
                    Url = model.Url,
                    imageUrl = model.imageUrl
                };

                _foodService.Create(entity);
                return RedirectToAction("FoodList", "Admin");
            }
            ViewBag.FoodCategoryIds = new SelectList(_foodCategoryService.GetAll(), "FoodCategoryId", "Name");
            return View(model);




        }

        public IActionResult CreateFoodCategory()
        {

            
            return View();
        }

        [HttpPost]
        public IActionResult CreateFoodCategory(FoodCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new FoodCategory()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Url = model.Url,
                    imageUrl = model.imageUrl
                };

                _foodCategoryService.Create(entity);
                return RedirectToAction("FoodCategoryList", "Admin");
            }
            
            return View(model);
        }



        public IActionResult EditFood(int? id)
        {
            if (id == null)
                return NotFound();
            var entity = _foodService.GetById((int)id);

            if (entity == null)
                return NotFound();

            var model = new FoodModel()
            {
                Id = entity.Id,
                FoodCategoryId = entity.FoodCategoryId,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                imageUrl = entity.imageUrl,
                Url = entity.Url,
                isApproved = entity.isApproved

            };
            ViewBag.FoodCategoryIds = _foodCategoryService.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult EditFood(FoodModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _foodService.GetById(model.Id);
                entity.FoodCategoryId = model.FoodCategoryId;
                entity.isApproved = model.isApproved;
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Description = model.Description;
                entity.imageUrl = model.imageUrl;
                entity.Url = model.Url;

                _foodService.Update(entity);
                return RedirectToAction("FoodList");
            }
            ViewBag.FoodCategoryIds = _foodCategoryService.GetAll();
            return View(model);
        }

        public IActionResult EditFoodCategory(int? id)
        {
            if (id == null)
                return NotFound();
            var entity = _foodCategoryService.GetByIdWithFoods((int)id);
            
            if (entity == null)
                return NotFound();


            var model = new FoodCategoryModel()
            {
                
                FoodCategoryId = entity.FoodCategoryId,
                Name = entity.Name,
              
                Description = entity.Description,
                imageUrl = entity.imageUrl,
                Url = entity.Url,
                foods = entity.Foods
               
            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult EditFoodCategory(FoodCategoryModel model)
        {
            if(ModelState.IsValid)
            {
                var entity = _foodCategoryService.GetById(model.FoodCategoryId);
                entity.Name = model.Name;
                entity.imageUrl = model.imageUrl;
                entity.Url = model.Url;
                entity.Description = model.Description;
                _foodCategoryService.Update(entity);
                return RedirectToAction("FoodCategoryList");
            }
            return View(model);
        }

        //[HttpPost]
        //public IActionResult DeleteFromFoodCategory(int? id)
        //{
        //    _foodCategoryService.DeleteFromFoodCategory((int)id);
           
        //    return RedirectToAction("FoodCategoryList");
        //}


        [HttpPost]
        public IActionResult DeleteFood(int? id)
        {
            var entity = _foodService.GetById((int)id);
            _foodService.Delete(entity);
            return RedirectToAction("FoodList");
        }

        [HttpPost]
        public IActionResult DeleteFoodCategory(int? FoodCategoryId)
        {
            var entity = _foodCategoryService.GetById((int)FoodCategoryId);
            _foodCategoryService.Delete(entity);
            return RedirectToAction("FoodCategoryList");
        }
    }
}
