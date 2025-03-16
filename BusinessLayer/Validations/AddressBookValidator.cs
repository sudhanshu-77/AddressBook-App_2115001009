using FluentValidation;
using ModelLayer.DTO;

namespace BusinessLayer.Validations
{
    public class AddressBookValidator : AbstractValidator<AddressBookDTO>
    {
        public AddressBookValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MaximumLength(100);
            RuleFor(a => a.Email).NotEmpty().EmailAddress();
            RuleFor(a => a.PhoneNumber).NotEmpty().MaximumLength(15);
            RuleFor(a => a.Address).MaximumLength(250);
        }
    }
}