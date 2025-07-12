using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Domain.Entities
{
    public class DiscountProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DiscountId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("DiscountId")]
        public Discount? Discount { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
