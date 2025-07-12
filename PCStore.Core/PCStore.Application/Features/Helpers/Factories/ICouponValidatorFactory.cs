using PCStore.Application.Features.Helpers.Validators.CouponValidator;
using PCStore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Factories
{
    public interface ICouponValidatorFactory
    {
        ICouponValidator GetValidator(CouponTargetType targetType);
    }
}
