using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Data;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
