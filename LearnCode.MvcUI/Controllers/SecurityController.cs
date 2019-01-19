using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LearnCode.Entities;
using LearnCode.MvcUI.Models;
using LLearnCode.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnCode.MvcUI.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IUser _user;
        private readonly IRole _role;

        public SecurityController(IUser user ,IRole role)
        {
            _user = user;
            _role = role;
        }

        public IActionResult login()
        {
            User model = new User();

            return View(model);
        }

        [HttpPost]
        public IActionResult login(User user)
        {
            if (ModelState.IsValid)
            {
               string userkey = Security.HashCompute(user.UserName, "leo");
                string Passwordkey = Security.HashCompute(user.Password, "leo");
             var loginUser=  _user.GetList(u => u.UserName == userkey && u.Password==Passwordkey).Result.SingleOrDefault();

                if (loginUser!=null)
                {
                    Claim UserName = new Claim(ClaimTypes.Name, user.UserName);
                    Claim Role = new Claim(ClaimTypes.Role, "Admin");
                    List<Claim> claims = new List<Claim> { UserName, Role };
                    var Identity = new ClaimsIdentity(claims);

                    HttpContext.User = new ClaimsPrincipal(Identity);

                    return RedirectToAction("list", "lesson"); 
                }
                else
                {
                    TempData["Error"] = "Username or Password not correct !";
                    return View();
                }

            }
            return View();
        }

        
        public IActionResult Add()
        {

            ListViewModel model = new ListViewModel();
            model.Roles = _role.GetList().Result;

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(User User)
        {
            if (ModelState.IsValid)
            {
                string userkey = Security.HashCompute(User.UserName, "leo");
                string Passwordkey = Security.HashCompute(User.Password, "leo");
                User.UserName = userkey;
                User.Password = Passwordkey;
                _user.Add(User);
                _user.Save();
                return RedirectToAction("login", "security");
            }
            else
            {
                TempData["Error"] = "İşlem yapılamadı";
                return View();
            }
            
        }

    }
}