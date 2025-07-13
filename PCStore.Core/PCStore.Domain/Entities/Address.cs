using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? AddressName { get; set; }

        [ForeignKey("User")]
        public required string UserId { get; set; }
        public User? User { get; set; }

        public required string Description { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
