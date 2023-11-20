using Experimentum.Client.Features.Emails;
using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Businesses;
using FluentValidation;

namespace Experimentum.Client.Features.Businesses
{
    public class BusinessRequestValidator : AbstractValidator<BusinessRequest>
    {
        public BusinessRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(business => business.Name)
                .NotEmpty()
                .Length(Business.MinimumLength, Business.MaximumLength);

            RuleFor(business => business.Email)
                .NotEmpty()
                .SetValidator(new EmailRequestValidator());
        }
    }
}
