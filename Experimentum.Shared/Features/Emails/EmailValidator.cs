using Experimentum.Domain.Features;
using FluentValidation;

namespace Experimentum.Shared.Features.Emails
{
    public class EmailValidator : AbstractValidator<EmailRequest>
    {
        public EmailValidator()
        {
            RuleFor(email => email)
            .MustBeValueObject(
                request => Email.Create(request.Address));
        }
    }
}