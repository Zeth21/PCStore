using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands
{
    public class DiscountUsageCalculatorCommand
    {
        public required string UserId { get; set; }
    }
}
