using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.ModelsDb;
using SoftUniBazar.Models;
using System.Globalization;
using System.Security.Claims;
using static SoftUniBazar.Constants.AdConstants;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly BazarDbContext context;

        public AdController(BazarDbContext _context)
        {
            context = _context;
        }

        [HttpGet, ActionName("All")]
        public async Task<IActionResult> Index()
        {
            var ads = await context
               .Ads
               .AsNoTracking()
               .Select(a => new AdViewModel(

                   a.Id,
                   a.Name,
                   a.ImageUrl,
                   a.CreatedOn.ToString(AdDateFormat),
                   a.Category.Name,
                   a.Description,
                   a.Price.ToString(),
                   a.Owner.UserName
               ))
               .ToListAsync();

            return View(ads);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id)
        {
            var ad = await context.Ads
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (ad == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ab = await context
                .AdBuyers
                .Where(ab => ab.AdId == id)
                .FirstOrDefaultAsync(ab => ab.BuyerId == userId);

            if (ab == null)
            {
                var adbuyer = new AdBuyer()
                {
                    AdId = id,
                    BuyerId = userId
                };
                await context.AdBuyers.AddAsync(adbuyer);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Cart));
            }

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string userId = GetUserId();

            var model = await context.AdBuyers
                .Where(ab => ab.BuyerId == userId)
                .AsNoTracking()
                .Select(a => new AdViewModel(
                    a.Ad.Id,
                    a.Ad.Name,
                    a.Ad.ImageUrl,
                    a.Ad.CreatedOn.ToString(AdDateFormat),
                    a.Ad.Category.Name,
                    a.Ad.Description,
                    a.Ad.Price.ToString(),
                    a.Ad.Owner.UserName
                    ))
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var e = await context.Ads
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ab = await context
                .AdBuyers
                .Where(ab => ab.AdId == id)
                .FirstOrDefaultAsync(ab => ab.BuyerId == userId);

            if (ab == null)
            {
                return BadRequest();
            }

            context.AdBuyers.Remove(ab);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AdFormViewModel();
            model.Categories = await GetAllCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetAllCategories();
                return View(model);
            }

            var entity = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CreatedOn = DateTime.Now,
                OwnerId = GetUserId()
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ad = await context
                .Ads.FindAsync(id);

            if (ad == null)
            {
                return BadRequest();
            }

            if (ad.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new AdFormViewModel()
            {
                Name = ad.Name,
                Description = ad.Description,
                ImageUrl = ad.ImageUrl,
                Price = ad.Price,
                CategoryId = ad.CategoryId
            };

            model.Categories = await GetAllCategories();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdFormViewModel model, int id)
        {
            var ad = await context
                .Ads.FindAsync(id);

            if (ad == null)
            {
                return BadRequest();
            }

            if (ad.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetAllCategories();

                return View(model);
            }

            ad.Name = model.Name;
            ad.Description = model.Description;
            ad.ImageUrl = model.ImageUrl;
            ad.Price = model.Price;
            ad.CategoryId = model.CategoryId;

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
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
