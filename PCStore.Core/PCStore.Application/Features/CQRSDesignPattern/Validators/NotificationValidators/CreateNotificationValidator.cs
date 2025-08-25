using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.NotificationValidators
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.NotificationTitle)
                .MaximumLength(30).WithMessage("Title cannot exceed 30 characters!");
            RuleFor(x => x.NotificationContent)
                .MaximumLength(250).WithMessage("Content cannot exceed 250 characters!");
        }
    }
}
