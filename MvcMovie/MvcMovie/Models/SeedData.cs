using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static async Task AddMovies(IServiceProvider serviceProvider)
        {
            using (MvcMovieDbContext context = new MvcMovieDbContext(serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieDbContext>>()))
            {
                if (await context.Movie.AnyAsync())
                {
                    return;
                }
                await context.Movie.AddRangeAsync(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Rating = "R",
                    Price = 7.99M
                },
                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Rating = "R",
                    Price = 8.99M
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Rating = "R",
                    Price = 9.99M
                },
                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Rating = "R",
                    Price = 3.99M
                }
            );
                await context.SaveChangesAsync();
            }
        }
        
        public static async Task AddRoles(IServiceProvider serviceProvider)
        {
            using (RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
            {
                string[] roles = new[] { "Admin", "Manager", "Member" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        public static async Task AddManager(IServiceProvider serviceProvider)
        {
            using (UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>())
            {
                string email = "peshko@test.com";
                string password = "123456";

                string[] roles = new[] { "Admin", "Manager"};

                if (await userManager.FindByEmailAsync(email) == null) 
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;

                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRolesAsync(user, roles);
                }
            }
        }
    }
}
