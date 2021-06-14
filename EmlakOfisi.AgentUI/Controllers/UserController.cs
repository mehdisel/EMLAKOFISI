using EmlakOfisi.BLL.Abstract;
using EmlakOfisi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmlakOfisi.AgentUI.Controllers
{
    [Authorize(Roles = "User,Admin")]
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
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOut();

            return RedirectToAction("Index", "Home");
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
        public IActionResult ChangePassword()
        {
            return View(new UserChangePasswordViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel userChangePasswordViewModel)
        {
            var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (user != null)
            {
               if (ModelState.IsValid)
               {
                    var result = await _userService.ChangePassword(userChangePasswordViewModel, int.Parse(user.Value));
                    if (result.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", result.Message);
                        return View("ChangePassword", userChangePasswordViewModel);
                    }
                }
            }
            return View("ChangePassword", userChangePasswordViewModel);
        }
    }
}
