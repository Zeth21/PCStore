using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.NotificationValidators
{
    public class MarkNotificationAsSeenValidator : AbstractValidator<MarkNotificationAsSeenCommand>
    {
        public MarkNotificationAsSeenValidator() 
        {
            RuleFor(x => x.NotificationId)
                .GreaterThan(0).WithMessage("Invalid notification id!");
        }
    }
}
