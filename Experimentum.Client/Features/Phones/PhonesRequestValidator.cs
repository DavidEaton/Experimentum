using Experimentum.Shared.Features.Phones;
using FluentValidation;

namespace Experimentum.Client.Features.Phones
{
    public class PhonesRequestValidator : AbstractValidator<List<PhoneRequest>>
    {
        public PhonesRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleForEach(phones => phones)
                .SetValidator(new PhoneRequestValidator());
        }
    }
}
