using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AutenticationAuthorization.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutenticationAuthorization.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
