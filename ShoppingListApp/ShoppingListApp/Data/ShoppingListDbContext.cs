using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Data.Models;

namespace ShoppingListApp.Data
{
    public class ShoppingListDbContext : DbContext
    {
        public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductNote> ProductNotes { get; set; }
    }
}
