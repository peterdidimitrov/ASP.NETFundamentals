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
            var movies = await context.Movies.AsNoTracking().Select(m => new MovieViewModel()
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
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
            {
                return BadRequest("Movie not found.");
            }

            string userId = GetUserId();

            var user = await context.Users
                                     .Include(u => u.UsersMovies)
                                     .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (user.UsersMovies.Any(um => um.MovieId == movieId))
            {
                return RedirectToAction(nameof(All));
            }

            var userMovie = new UserMovie()
            {
                UserId = userId,
                MovieId = movieId
            };

            user.UsersMovies.Add(userMovie);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Watched()
        {
            var userId = GetUserId();

            var movies = await context.Movies
                .AsNoTracking()
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

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
            {
                return BadRequest("Movie not found.");
            }

            string userId = GetUserId();

            var user = await context.Users
                                     .Include(u => u.UsersMovies)
                                     .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var userMovieToRemove = user.UsersMovies.FirstOrDefault(um => um.MovieId == movieId);
            if (userMovieToRemove != null)
            {
                user.UsersMovies.Remove(userMovieToRemove);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Watched));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new MovieFormViewModel();
            model.Genres = await GetAllGenres();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await GetAllGenres();
                return View(model);
            }

            var entity = new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                GenreId = model.GenreId
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IEnumerable<GenreViewModel>> GetAllGenres()
        {
            return await context
                .Genres
                .AsNoTracking()
                .Select(c => new GenreViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
