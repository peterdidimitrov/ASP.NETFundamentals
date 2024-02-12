using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models;
using TaskBoardApp.Models.Task;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly TaskBoardAppDbContext context;

        public TaskController(TaskBoardAppDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel taskModel = new TaskFormModel()
            {
                Boards = await GetBoards(),
            };
            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel taskModel)
        {
            if (!(await GetBoards()).Any(b => b.Id == taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist.");
            }

            string currentUserId = GetUserId();

            if (!ModelState.IsValid)
            {
                taskModel.Boards = await GetBoards();

                return View(taskModel);
            }

            Task task = new Task()
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                CreatedOn = DateTime.Now,
                BoardId = taskModel.BoardId,
                OwnerId = currentUserId
            };

            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();


            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var task = await context
                .Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName
                })
                .FirstOrDefaultAsync();

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await context.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();

            if (currentUserId != task.OwnerId) 
            {
                return Unauthorized();
            }

            TaskFormModel taskModel = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = await GetBoards()
            };

            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskFormModel taskFormModel)
        {
            var task = await context.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();

            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            if (!(await GetBoards()).Any(b => b.Id == taskFormModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskFormModel.BoardId), "Board does not exist.");
            }

            if (!ModelState.IsValid)
            {
                taskFormModel.Boards = await GetBoards();

                return View(taskFormModel);
            }

            task.Title = taskFormModel.Title;
            task.Description = taskFormModel.Description;
            task.BoardId = taskFormModel.BoardId;

            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await context.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();

            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel taskModel = new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description
            };

            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel taskModel)
        {
            var task = await context.Tasks.FindAsync(taskModel.Id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();

            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            context.Tasks.Remove(task);

            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Board");
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private async Task<IEnumerable<TaskBoardModel>> GetBoards()
        {
            return await context
                .Boards
                .Select(b => new TaskBoardModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToListAsync();
        }
    }
}
