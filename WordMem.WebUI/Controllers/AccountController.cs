using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WordMem.Business.Abstract;
using WordMem.CrossCutting.DTOs;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;

namespace WordMem.Controllers
{
    
    public class AccountController : Controller
    {
        private IAuthService authService;
       

        public AccountController(IAuthService _authService)
        {
            authService = _authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO user)
        {

            if (ModelState.IsValid)
            {

                var result = await authService.Register(user);

                if (result.Succeeded)
                {
                    LoginDTO loginDTO = new LoginDTO()
                    {
                        Email = user.Email,
                        Password=user.Password                       

                    };
                    var login = await authService.LogginUser(loginDTO);

                    if (login != null)
                    {
                        return Redirect("/Dashboard");
                    }
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(user);

        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string returnUrl, LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await authService.LogginUser(model);

                if (result.Succeeded)
                {
                        return Redirect(returnUrl ?? "/Dashboard");
                }
                ModelState.AddModelError("Email", "Invalid email or password");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return Redirect("/");
        }
    }
}