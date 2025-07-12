using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands
{
    public class DiscountValidatorCommand
    {
        public int ProductId { get; set; }
        public required decimal ProductPrice { get; set; }
    }
}
