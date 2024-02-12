using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Home;
using Microsoft.EntityFrameworkCore;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskBoardAppDbContext context;

        public HomeController(TaskBoardAppDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            string[] taskBoard = context.Boards.Select(b => b.Name).Distinct().ToArray();

            List<HomeBoardModel> tasksCounts = new List<HomeBoardModel>();
            foreach (var boardName in taskBoard)
            {
                int tasksInBoard = context.Tasks.Where(t => t.Board.Name == boardName).Count();

                tasksCounts.Add(new HomeBoardModel
                {
                    BoardName = boardName,
                    TasksCount = tasksInBoard
                });
            }

            int userTasksCount = -1;

            if (User.Identity.IsAuthenticated)
            {
                string? currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userTasksCount = context.Tasks.Where(t => t.OwnerId == currentUserId).Count();
            }
            HomeViewModel? homeModel = new HomeViewModel()
            {
                AllTasksCount = context.Tasks.Count(),
                BoardsWithTasksCount = tasksCounts,
                UserTasksCount = userTasksCount
            };

            return View(homeModel);
        }
    }
}
