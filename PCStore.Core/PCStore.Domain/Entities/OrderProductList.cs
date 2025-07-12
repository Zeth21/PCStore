using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class OrderProductList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ForeignKey("OrderId")]
        public int ListId { get; set; }
        public int ProductId { get; set; }
        public byte ProductQuantity { get; set; }
        public decimal ProductTotalCost { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal? ProductOldPrice { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

    }
}
