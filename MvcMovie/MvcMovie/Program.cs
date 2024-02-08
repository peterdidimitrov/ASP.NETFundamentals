using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MvcMovie
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder
                .Services
                .AddDbContext<MvcMovieDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

            // Add Identity services
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Set to false to disable email confirmation
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MvcMovieDbContext>()
                .AddDefaultTokenProviders();

            // Add IEmailSender services
            builder.Services.AddTransient<IEmailSender, NullEmailSender>();

            // Add services to the container.
            builder
                .Services
                .AddControllersWithViews();

            // Add Razor Pages
            builder
                .Services
                .AddRazorPages();

            WebApplication app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Add this line for Identity

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                // Seed movies if needed
                await SeedData.AddMovies(services);

                // Seed roles if needed
                await SeedData.AddRoles(services);

                // Seed Manager
                await SeedData.AddManager(services);
            }

            app.Run();
        }
    }
}
    

