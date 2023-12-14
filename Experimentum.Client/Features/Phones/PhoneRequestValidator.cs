using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Phones;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Experimentum.Client.Features.Phones
{
    public class PhoneRequestValidator : AbstractValidator<PhoneRequest>
    {
        public PhoneRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(phone => phone.Number)
                .NotEmpty()
                .WithMessage(Phone.EmptyMessage)
                .Must(BeAValidPhoneNumber)
                .WithMessage(Phone.InvalidMessage);
        }

        private bool BeAValidPhoneNumber(string number)
        {
            var phoneAttribute = new PhoneAttribute();
            return phoneAttribute.IsValid(number);
        }
    }
}