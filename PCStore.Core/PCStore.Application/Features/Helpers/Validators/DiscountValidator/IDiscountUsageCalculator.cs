using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Validators.DiscountValidator
{
    public interface IDiscountUsageCalculator 
    {
        Task<List<DiscountUsageCalculatorResult>> CalculateDiscountUsage(DiscountUsageCalculatorCommand command);
    }
}
