using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Domain.Entities
{
    public class ShoppingCartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public required string UserId { get; set; }

        [ForeignKey("Product")]
        public required int ProductId { get; set; }

        public int ItemCount { get; set; } = 1;

        public Product? Product { get; set; }
        public User? User { get; set; }

    }
}
