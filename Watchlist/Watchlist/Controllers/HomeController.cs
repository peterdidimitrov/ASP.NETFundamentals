using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Watchlist.Data.Models;

namespace Watchlist.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<User> signInManager;

        public HomeController(SignInManager<User> _signInManager)
        {
            signInManager = _signInManager;
        }

        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("All", "Movies");
            }

            return View();
        }
    }
}