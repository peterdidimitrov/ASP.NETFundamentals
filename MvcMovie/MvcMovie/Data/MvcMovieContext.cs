using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvcMovie.Models;
using MvcMovie.Constants;

namespace MvcMovie.Data
{
    public class MvcMovieContext : IdentityDbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguranion());
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}
public class ApplicationUserEntityConfiguranion : IEntityTypeConfiguration<SampleUser>
{
    public void Configure(EntityTypeBuilder<SampleUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(UserModelConstants.FirstNameMaxLength);
        builder.Property(x => x.LastName).HasMaxLength(UserModelConstants.LastNameMaxLength);
    }
}