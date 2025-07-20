using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        [Column(TypeName = "smallmoney")]
        public required decimal ProductPrice { get; set; }
        public string? ProductMainPhotoPath { get; set; }
        public short ProductStock { get; set; }
        public int ProductBrandId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int ProductCategoryId { get; set; }
        public bool ProductIsAvailable { get; set; } = true;
        public int ProductTotalRate { get; set; } = 0;
        public decimal ProductRateScore { get; set; } = 0;

        public int ProductTypeId { get; set; }

        [ForeignKey("ProductBrandId")]
        public Brand? Brand { get; set; }

        [ForeignKey("ProductCategoryId")]
        public Category? Category { get; set; }
        public ICollection<CouponProduct>? CouponProducts { get; set; }
        public ICollection<OrderProductList>? OrderProductLists { get; set; }
        public ICollection<DiscountProduct>? DiscountProducts { get; set; }
        public ICollection<ProductRate>? ProductRates { get; set; }
        public ICollection<Comment>? ProductComments { get; set; }
        public ICollection<ProductPhoto>? ProductPhotos { get; set; }
        public ICollection<FollowedProduct>? FollowedProducts { get; set; }
        public ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductType? ProductType { get; set; }
    }
}
