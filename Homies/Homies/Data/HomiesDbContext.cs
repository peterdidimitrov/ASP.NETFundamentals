using Homies.Data.ModelsDb;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Type = Homies.Data.ModelsDb.Type;

namespace Homies.Data
{
    public class HomiesDbContext : IdentityDbContext
    {
        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventParticipant>(e => e.HasKey(ep => new
            {
                ep.HelperId,
                ep.EventId
            }));

            modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.Event)
            .WithMany(e => e.EventsParticipants)
            .HasForeignKey(ep => ep.EventId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Type>()
                .HasData(new Type()
                {
                    Id = 1,
                    Name = "Animals"
                },
                new Type()
                {
                    Id = 2,
                    Name = "Fun"
                },
                new Type()
                {
                    Id = 3,
                    Name = "Discussion"
                },
                new Type()
                {
                    Id = 4,
                    Name = "Work"
                });

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<ModelsDb.Type> Types { get; set; } = null!;
        public virtual DbSet<EventParticipant> EventParticipants { get; set; } = null!;
    }
}