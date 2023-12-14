using Experimentum.Domain.Features;
using FluentValidation;

namespace Experimentum.Shared.Features.Phones
{
    public class PhoneRequest : AbstractValidator<PhoneRequest>
    {
        public long Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public PhoneType PhoneType { get; set; } = PhoneType.Other;
    }
}
