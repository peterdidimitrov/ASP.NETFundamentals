using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.ModelsDb;
using SeminarHub.Models;
using System.Globalization;
using System.Security.Claims;
using static SeminarHub.Constants.SeminarConstants;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext context;

        public SeminarController(SeminarHubDbContext _context)
        {
            this.context = _context;
        }


        [HttpGet, ActionName("All")]
        public async Task<IActionResult> Index()
        {

            var events = await context
                .Seminars
                .AsNoTracking()
                .Select(s => new SeminarViewModel(
                    s.Id,
                    s.Topic,
                    s.Lecturer,
                    s.DateAndTime,
                    s.Category.Name,
                    s.Organizer.UserName
                ))
                .ToListAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SeminarFormViewModel();
            model.Categories = await GetAllCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormViewModel model)
        {
            DateTime start = DateTime.Now;

            if (!DateTime.TryParseExact(model.DateAndTime, SeminarDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {SeminarDateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetAllCategories();
                return View(model);
            }

            var entity = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                OrganizerId = GetUserId(),
                DateAndTime = start,
                Duration = model.Duration,
                CategoryId = model.CategoryId
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var seminar = await context.Seminars
                .Where(e => e.Id == id)
                .Include(e => e.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (seminar == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!seminar.SeminarsParticipants.Any(p => p.ParticipantId == userId))
            {
                seminar.SeminarsParticipants.Add(new SeminarParticipant()
                {
                    SeminarId = id,
                    ParticipantId = userId
                });

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Joined));
            }

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await context.SeminarParticipants
                .Where(sp => sp.ParticipantId == userId)
                .AsNoTracking()
                .Select(sp => new SeminarViewModel(
                    sp.Seminar.Id,
                    sp.Seminar.Topic,
                    sp.Seminar.Lecturer,
                    sp.Seminar.DateAndTime,
                    sp.Seminar.Category.Name,
                    sp.Seminar.Organizer.UserName
                ))
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var e = await context.Seminars
                .Where(s => s.Id == id)
                .Include(s => s.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ep = e.SeminarsParticipants.FirstOrDefault(ep => ep.ParticipantId == userId);

            if (ep == null)
            {
                return BadRequest();
            }

            e.SeminarsParticipants.Remove(ep);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var seminar = await context
                .Seminars.FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new SeminarFormViewModel()
            {
                Topic = seminar.Topic,
                Lecturer = seminar.Lecturer,
                Details = seminar.Details,
                DateAndTime = seminar.DateAndTime.ToString(SeminarDateFormat, CultureInfo.InvariantCulture),
                Duration = (int)seminar.Duration,
                CategoryId = seminar.CategoryId
            };

            model.Categories = await GetAllCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeminarFormViewModel model, int id)
        {
            var seminar = await context
                .Seminars.FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            DateTime start = DateTime.Now;

            if (!DateTime.TryParseExact(model.DateAndTime, SeminarDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {SeminarDateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetAllCategories();

                return View(model);
            }

            seminar.Topic = model.Topic;
            seminar.Lecturer = model.Lecturer;
            seminar.Details = model.Details;
            seminar.DateAndTime = start;
            seminar.Duration = model.Duration;
            seminar.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await context
                .Seminars
                .Where(s => s.Id == id)
                .AsNoTracking()
                .Select(s => new SeminarDetailsViewModel(s.Id, s.Topic, s.Lecturer, s.Details, s.DateAndTime.ToString(SeminarDateFormat), s.Duration.ToString(), s.Category.Name, s.Organizer.UserName))
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var seminar = await context.Seminars.FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();

            if (currentUserId != seminar.OrganizerId)
            {
                return Unauthorized();
            }

            SeminarDeleteViewModel seminarModel = new SeminarDeleteViewModel(
                    seminar.Id,
                    seminar.Topic,
                    seminar.DateAndTime
                );

            return View(seminarModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seminar = await context.Seminars
                .Include(s => s.SeminarsParticipants)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (seminar == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();

            if (currentUserId != seminar.OrganizerId)
            {
                return Unauthorized();
            }

            if (seminar.SeminarsParticipants.Any())
            {
                context.SeminarParticipants.RemoveRange(seminar.SeminarsParticipants);
            }

            context.Seminars.Remove(seminar);

            await context.SaveChangesAsync();
            return RedirectToAction("All");
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
        private async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            return await context
                .Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel(c.Name)
                {
                    Id = c.Id,
                })
                .ToListAsync();
        }
    }
}
