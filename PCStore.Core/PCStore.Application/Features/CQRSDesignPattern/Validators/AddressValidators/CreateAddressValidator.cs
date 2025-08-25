using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AddressCommands;
namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AddressValidators
{
    public class CreateAddressValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressValidator() 
        {
            RuleFor(x => x.AddressName)
                .NotEmpty().WithMessage("Address name must not be empty")
                .MaximumLength(50).WithMessage("Address name cannot exceed 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description must not be empty")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");
        }
    }
}
