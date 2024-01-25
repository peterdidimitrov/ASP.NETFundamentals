using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListApp.Data.Models
{
    [Comment("Shopping  List Product")]
    public class Product
    {
        [Key]
        [Comment("Product Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Comment("Product Name")]
        public required string Name { get; set; }

        [Comment("List of ProductNotes")]
        public required List<ProductNote> ProductNote { get; set; }


    }
}
