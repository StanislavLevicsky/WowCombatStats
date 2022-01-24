using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;
using WowCombatStats.Domain;
using WowCombatStats.Models.VIewModels;

namespace WowCombatStats.App.Controllers
{
    public class AuthController : Controller
    {
        private readonly Auth _auth;

        public AuthController(Auth auth)
        {
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var url = Request.Headers["Referer"].ToString();

            if (url == null)
            {
                returnUrl = "/Home/Index";
            }
            else
            {
                returnUrl = new Regex(@"(.*):(\d*)").Replace(url, string.Empty);
            }

            if (returnUrl == "/Auth/Registration")
            {
                returnUrl = "/Home/Index";
            }

            return View(new LoginVM { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var result = _auth.Login(loginVM);
            if (result)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(_auth.GetIdentity(loginVM.Login)), new AuthenticationProperties { IsPersistent = loginVM.isPersistent});
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username and/or password.");
            }

            return View(loginVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            _auth.Logout(HttpContext.User);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration(string returnUrl = null)
        {
            var url = Request.Headers["Referer"].ToString();

            if (url == null)
            {
                returnUrl = "/Home/Index";
            }
            else
            {
                returnUrl = new Regex(@"(.*):(\d*)").Replace(url, string.Empty);
            }

            if (returnUrl == "/Auth/Login ")
            {
                returnUrl = "/Home/Index";
            }

            return View(new UserRegistrationVM { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserRegistrationVM userRegistrationVM)
        {
            if (ModelState.IsValid)
            {
                var result = _auth.Registration(userRegistrationVM);

                if (result)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                                                  new ClaimsPrincipal(_auth.GetIdentity(userRegistrationVM.Login)));
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(userRegistrationVM);
        }

        [HttpGet]
        public IActionResult LoginIsExist(string login)
        {
            return Json(_auth.LoginIsExist(login));
        }
    }
}
