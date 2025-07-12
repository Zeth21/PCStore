using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Domain.Entities
{
    public class CouponProductType
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public int CouponId { get; set; }


        [ForeignKey("CouponId")]
        public Coupon? Coupon { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductType? ProductType { get; set; }
    }
}
