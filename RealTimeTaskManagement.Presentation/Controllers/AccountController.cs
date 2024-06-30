using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTimeTaskManagement.Data.Context;
using RealTimeTaskManagement.Data.Entities;
using System.Security.Claims;

namespace RealTimeTaskManagement.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly RealTimeTaskManagementDbContext _applicationDbContext;
        public AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signManager, RealTimeTaskManagementDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signManager;
            _applicationDbContext = applicationDbContext;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
