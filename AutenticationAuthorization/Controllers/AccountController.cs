using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutenticationAuthorization.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AutenticationAuthorization.Controllers
{
    public class AccountController : Controller
    {        
        public IActionResult Login(string requestPath)
        {
            ViewBag.RequestPath = requestPath ?? "/";
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel inputModel)
        {
            if (!IsAuthentic(inputModel.Username, inputModel.Password))
                return View();

            // create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Sean Connery"),
                new Claim(ClaimTypes.Email, inputModel.Username)
            };

            // create identity
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

            // create principal
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // sign-in
            await HttpContext.SignInAsync(
                    scheme: "BeltranScheme",
                    principal: principal,
                    properties: new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                    {
                        //IsPersistent = true, // for 'remember me' feature
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(2)
                    });

            return Redirect(inputModel.RequestPath ?? "/");
            //return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync(
                    scheme: "BeltranScheme");

            return RedirectToAction("Login");
        }

        public IActionResult Access()
        {
            return View();
        }    

        private bool IsAuthentic(string username, string password)
        {
            return (username == "beltran" && password == "1234");
        }
    }

}