using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Validators.DiscountValidator.Results
{
    public class DiscountValidatorResult
    {
        public int ProductId { get; set; }
        public required decimal ProductPrice { get; set; }
        public decimal? OldPrice { get; set; }
        public bool? IsDiscountPercentage { get; set; }
        public decimal? DiscountRate { get; set; }
    }
}
