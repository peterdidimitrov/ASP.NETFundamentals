using Microsoft.AspNetCore.Mvc;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IdentityDbContext context;

        //public HomeController(IdentityDbContext _context)
        //{
        //    context = _context;
        //}

        public IActionResult Index()
        {
            return View();
        }
    }
}
