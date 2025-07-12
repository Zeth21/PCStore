using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Domain.Entities
{
    public class DiscountUsage
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DiscountId { get; set; }
        public decimal DiscountTotal { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        [ForeignKey("DiscountId")]
        public Discount? Discount { get; set; }
    }
}
