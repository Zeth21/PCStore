using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public decimal OrderTotalCost { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? OrderDeliverDate { get; set; }
        public bool OrderIsActive { get; set; }
        public int OrderAddressId { get; set; }
        public string? OrderUserId { get; set; }


        [ForeignKey(nameof(OrderAddressId))]
        public Address? Address { get; set; }
        public OrderProductList? OrderProductList { get; set; }

        [ForeignKey("OrderUserId")]
        public User? User { get; set; }

        public ICollection<OrderStatus>? OrderStatus { get; set; }
        public CouponUsage? CouponUsage { get; set; }
        public DiscountUsage? DiscountUsage { get; set; }
    }
}
