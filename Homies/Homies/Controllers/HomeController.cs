using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homies.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(SignInManager<IdentityUser> _signInManager)
        {
            this.signInManager = _signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("All", "Event");
            }
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}