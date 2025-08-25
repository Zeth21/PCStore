using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AddressCommands;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AddressValidators
{
    public class RemoveAddressValidator : AbstractValidator<RemoveAddressCommand>
    {
        public RemoveAddressValidator() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid address id!");
        }
    }
}
