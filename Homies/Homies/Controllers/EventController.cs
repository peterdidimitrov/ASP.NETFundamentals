using Homies.Data;
using Homies.Data.ModelsDb;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using static Homies.Constants.EventConstants;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext context;
        private readonly SignInManager<IdentityUser> signInManager;
        private UserManager<IdentityUser> userManager;

        public EventController(HomiesDbContext _context, SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager)
        {
            this.context = _context;
            this.signInManager = _signInManager;
            this.userManager = _userManager;
        }

        [HttpGet, ActionName("All")]
        public async Task<IActionResult> Index()
        {

            var events = await context
                .Events
                .AsNoTracking()
                .Select(e => new EventInfoViewModel(
                
                    e.Id,
                    e.Name,
                    e.Start,
                    e.Type.Name,
                    e.Organiser.UserName
                ))
                .ToListAsync();

            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var e = await context.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!e.EventsParticipants.Any(p => p.HelperId == userId))
            {
                e.EventsParticipants.Add(new EventParticipant()
                {
                    EventId = id,
                    HelperId = userId
                });

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await context.EventParticipants
                .Where(ep => ep.HelperId == userId)
                .AsNoTracking()
                .Select(ep => new EventInfoViewModel(
                    ep.EventId,
                    ep.Event.Name,
                    ep.Event.Start,
                    ep.Event.Type.Name,
                    ep.Event.Organiser.UserName
                    ))
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var e = await context.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ep = e.EventsParticipants.FirstOrDefault(ep => ep.HelperId == userId);

            if (ep == null)
            {
                return BadRequest();
            }

            e.EventsParticipants.Remove(ep);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventFormViewModel();
            model.Types = await GetTypes();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start, EventDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {EventDateFormat}");
            }

            if (!DateTime.TryParseExact(model.End, EventDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {EventDateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Types = await GetTypes();
                return View(model);
            }

            var entity = new Event() 
            {
                CreatedOn = DateTime.Now,
                Description = model.Description,
                Name = model.Name,
                OrganiserId = GetUserId(),
                TypeId = model.TypeId,
                Start = start, 
                End = end 
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await context
                .Events.FindAsync(id);

            if (e == null) 
            {
                return BadRequest(); 
            }

            if (e.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new EventFormViewModel()
            {
                Name = e.Name,
                Description = e.Description,
                Start = e.Start.ToString(EventDateFormat),
                End = e.End.ToString(EventDateFormat),
                TypeId = e.TypeId
            };

            model.Types = await GetTypes();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model, int id)
        {
            var e = await context
                .Events.FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start, EventDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {EventDateFormat}");
            }

            if (!DateTime.TryParseExact(model.End, EventDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {EventDateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Types = await GetTypes();

                return View(model);
            }

            e.Start = start;
            e.End = end;
            e.Name = model.Name;
            e.Description = model.Description;
            e.TypeId = model.TypeId;

            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await context
                .Events
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new EventDetailsViewModel(e.Id, e.Name, e.Description, e.Start.ToString(EventDateFormat), e.End.ToString(EventDateFormat), e.Type.Name, e.CreatedOn.ToString(EventDateFormat), e.Organiser.UserName))
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }


            return View(model);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
        private async Task<IEnumerable<TypeViewModel>> GetTypes()
        {
            return await context
                .Types
                .AsNoTracking()
                .Select(t => new TypeViewModel(t.Name)
                {
                    Id = t.Id,
                })
                .ToListAsync();
        }
    }
}
