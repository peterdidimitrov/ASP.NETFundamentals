using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Data.Models;
using MvcMovie.Models;
using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class MoviesController : Controller
    {
        private readonly MvcMovieDbContext context;
        private readonly SignInManager<IdentityUser> signInManager;
        private UserManager<IdentityUser> userManager;

        public MoviesController(MvcMovieDbContext _context, SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager)
        {
            this.context = _context;
            this.signInManager = _signInManager;
            this.userManager = _userManager;
        }

        // GET: Movies
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            if (signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Manager") || User.IsInRole("Admin"))
                {
                    if (context.Movie == null)
                    {
                        return Problem("Entity set 'MvcMovieContext.Movie' is null.");
                    }

                    // Use LINQ to get list of genres.
                    IQueryable<string> genreQuery = from m in context.Movie
                                                    orderby m.Genre
                                                    select m.Genre;

                    IQueryable<Movie>? movies = from m in context.Movie
                                                orderby m.Title, m.ReleaseDate
                                                select m;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        movies = movies.Where(s => s.Title!.StartsWith(searchString));
                    }

                    if (!string.IsNullOrEmpty(movieGenre))
                    {
                        movies = movies.Where(x => x.Genre == movieGenre);
                    }

                    MovieGenreViewModel? movieGenreVM = new MovieGenreViewModel
                    {
                        Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                        Movies = await movies.ToListAsync()
                    };

                    return View(movieGenreVM);
                }

                return RedirectToAction(nameof(GetAllMovies));
            }
            else
            {
                return RedirectToAction(nameof(NotAuthenticated));
            }
        }

        // GET: Movies/NotAuthenticated
        [HttpGet]
        [AllowAnonymous]
        public IActionResult NotAuthenticated()
        {
            return View();
        }

        // GET: Movies/Details
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/DetailsAndEdit
        [HttpGet]
        public async Task<IActionResult> DetailsAndEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                context.Add(movie);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(movie);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await context.Movie.FindAsync(id);
            if (movie != null)
            {
                context.Movie.Remove(movie);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetAllMovies(string movieGenre, string searchString)
        {
            if (context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie' is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            IQueryable<Movie>? movies = from m in context.Movie
                                        orderby m.Title, m.ReleaseDate
                                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.StartsWith(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            MovieGenreViewModel? movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }

        private bool MovieExists(int id)
        {
            return context.Movie.Any(e => e.Id == id);
        }
    }
}