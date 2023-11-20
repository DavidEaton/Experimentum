using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Emails;
using FluentValidation;

namespace Experimentum.Api.Features.Emails
{
    public class EmailRequestValidator : AbstractValidator<EmailRequest>
    {
        public EmailRequestValidator()
        {
            RuleFor(email => email)
            .MustBeValueObject(
                request => Email.Create(request.Address));
        }
    }
}