using EmlakOfisi.BLL.Abstract;
using EmlakOfisi.Entities.Concrete;
using EmlakOfisi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakOfisi.AdminUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View(new UserSignInViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserSignInViewModel userSignInViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(userSignInViewModel);
                
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View("Login", userSignInViewModel);
                }

            }
            return View("Login", userSignInViewModel);
        }
        
        public IActionResult Register()
        {

            return View(new UserSignUpViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserSignUpViewModel userSignUpViewModel)
        {
            if (ModelState.IsValid)
            {

                var result = await _userService.Register(userSignUpViewModel);
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View("Register", userSignUpViewModel);
                }
            }
            return View("Register", userSignUpViewModel);
        }


    }
}
