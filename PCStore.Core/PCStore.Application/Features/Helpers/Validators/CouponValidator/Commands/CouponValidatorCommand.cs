using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Validators.CouponValidator.Commands
{
    public class CouponValidatorCommand
    {
        public int ProductId { get; set; }
        public required decimal ProductPrice { get; set; }
        public int ProductBrandId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductTypeId { get; set; }
        public int ItemCount { get; set; }
    }
}
