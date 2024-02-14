using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.ModelsDb;
using SoftUniBazar.Models;
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
            }

            return RedirectToAction(nameof(Cart));
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

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
        //private async Task<IEnumerable<TypeViewModel>> GetTypes()
        //{
        //    return await context
        //        .Types
        //        .AsNoTracking()
        //        .Select(t => new TypeViewModel(t.Name)
        //        {
        //            Id = t.Id,
        //        })
        //        .ToListAsync();
        //}
    }
}
