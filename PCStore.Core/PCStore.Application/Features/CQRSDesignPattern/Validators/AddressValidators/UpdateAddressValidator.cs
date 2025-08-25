using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AddressCommands;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AddressValidators
{
    public class UpdateAddressValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressValidator()
        {
            RuleFor(x => x.AddressName)
                .MaximumLength(50)
                .When(x => x.AddressName != null)
                .WithMessage("Address name cannot exceed 50 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => x.Description != null)
                .WithMessage("Description cannot exceed 500 characters");
        }
    }
}
