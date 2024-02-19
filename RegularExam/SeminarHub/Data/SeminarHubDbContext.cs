using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data.ModelsDb;

namespace SeminarHub.Data
{
    public class SeminarHubDbContext : IdentityDbContext
    {
        public SeminarHubDbContext(DbContextOptions<SeminarHubDbContext> options)
            : base(options)
        {
        }

        public DbSet<Seminar> Seminars { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<SeminarParticipant> SeminarParticipants { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SeminarParticipant>(e => e.HasKey(ep => new
            {
                ep.SeminarId,
                ep.ParticipantId
            }));

            builder.Entity<SeminarParticipant>()
            .HasOne(ep => ep.Seminar)
            .WithMany(e => e.SeminarsParticipants)
            .HasForeignKey(ep => ep.SeminarId)
            .OnDelete(DeleteBehavior.NoAction);

            builder
               .Entity<Category>()
               .HasData(new Category()
               {
                   Id = 1,
                   Name = "Technology & Innovation"
               },
               new Category()
               {
                   Id = 2,
                   Name = "Business & Entrepreneurship"
               },
               new Category()
               {
                   Id = 3,
                   Name = "Science & Research"
               },
               new Category()
               {
                   Id = 4,
                   Name = "Arts & Culture"
               });

            base.OnModelCreating(builder);
        }
    }
}