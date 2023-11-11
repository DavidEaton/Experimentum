using Experimentum.Domain.Features;
using FluentValidation;

namespace Experimentum.Shared.Features.Emails
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