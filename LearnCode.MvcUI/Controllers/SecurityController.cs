using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearnCode.MvcUI.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(User user)
        {
            return View();
        }
    }
}