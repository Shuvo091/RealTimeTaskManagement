using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTimeTaskManagement.Data.Entities;
using RealTimeTaskManagement.Models;
using RealTimeTaskManagement.Models.ViewModels;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordModel)
        {
            var isReset = true;
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // TODO: Email send logic here
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SetOrResetPassword(string token, string email, bool isReset)
        {
            var model = new SetOrResetPasswordVM
            {
                Token = token,
                Email = email,
                IsReset = isReset,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SetOrResetPassword(SetOrResetPasswordVM resetPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordModel);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
                if (user != null)
                {
                    var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
                    if (!resetPassResult.Succeeded)
                    {
                        foreach (var error in resetPassResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(resetPasswordModel);
                    }
                    user.EmailConfirmed = true;
                    //TODO: Set password here
                }
                return RedirectToAction(nameof(SetOrResetPasswordConfirmation), new { isReset = resetPasswordModel.IsReset });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SetOrResetPasswordConfirmation(bool isReset)
        {
            ViewBag.IsReset = isReset;
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangeUserPassword()
        {
            var model = new ChangeUserPasswordVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordVM changeUserPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                changeUserPasswordVM.ConfirmPassword = string.Empty;
                return View(changeUserPasswordVM);
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var resetPassResult = await _userManager.ChangePasswordAsync(user, changeUserPasswordVM.CurrentPassword, changeUserPasswordVM.NewPassword);
                    if (!resetPassResult.Succeeded)
                    {
                        foreach (var error in resetPassResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(changeUserPasswordVM);
                    }
                }
                return RedirectToAction(nameof(ChangeUserPasswordConfirmation));
            }
        }

        [HttpGet]
        public IActionResult ChangeUserPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
