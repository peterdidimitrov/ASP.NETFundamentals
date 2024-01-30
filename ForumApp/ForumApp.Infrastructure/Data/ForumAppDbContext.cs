using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Infrastructure.Data
{
    public class ForumAppDbContext : DbContext
    {
        public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options) 
            : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder
            //    .Entity<Post>();

            base.OnModelCreating(builder);
        }
        public DbSet<Post> Posts { get; set; }
    }
}
