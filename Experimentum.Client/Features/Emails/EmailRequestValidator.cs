using Experimentum.Shared.Features.Emails;
using FluentValidation;

namespace Experimentum.Client.Features.Emails
{
    public class EmailRequestValidator : AbstractValidator<EmailRequest>
    {
        public EmailRequestValidator()
        {
            RuleFor(email => email.Address)
                .NotEmpty()
                .EmailAddress();
        }
    }
}