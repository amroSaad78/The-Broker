using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.ViewModels.AccountViewModels;

namespace WebMVC.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            var vm = new LoginViewModel {};
            return View(vm);
        }
    }
}