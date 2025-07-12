using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class FollowedProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public required string UserId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}
