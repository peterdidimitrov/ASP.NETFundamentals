using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly WatchlistDbContext context;

        public MoviesController(WatchlistDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var movies = await context.Movies.Select(m => new MovieViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                ImageUrl = m.ImageUrl,
                Rating = m.Rating,
                Genre = m.Genre.Name
            })
            .ToListAsync();

            return View(movies);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int movieId)
        {
            var movie = await context.Movies
                .Where(m => m.Id == movieId)
                .FirstOrDefaultAsync();

            string userId = GetUserId();

            var user = await context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                return BadRequest();
            }

            if (user == null)
            {
                return BadRequest();
            }

            if (movie.UsersMovies.Any(um => um.UserId == userId))
            {
                return RedirectToAction(nameof(All));
            }

            var userMovie = new UserMovie()
            {
                UserId = userId,
                MovieId = movie.Id
            };

            user.UsersMovies.Add(userMovie);
            movie.UsersMovies.Add(userMovie);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Watched()
        {
            var userId = GetUserId();

            var movies = await context.Movies
                .Where(m => m.UsersMovies.Any(u => u.UserId == userId))
                .Select(m => new MovieViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                ImageUrl = m.ImageUrl,
                Rating = m.Rating,
                Genre = m.Genre.Name
            })
            .ToListAsync();
            return View(movies);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
