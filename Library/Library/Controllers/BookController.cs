using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext context;

        public BookController(LibraryDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var movies = await context.Books.AsNoTracking().Select(m => new BookViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ImageUrl = m.ImageUrl,
                Author = m.Author,
                Rating = m.Rating,
                Category = m.Category.Name
            })
            .ToListAsync();

            return View(movies);
        }
    }
}
