using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Emails;
using Experimentum.Shared.Features.Persons.PersonNames;

namespace Experimentum.Shared.Features.Persons
{
    public class PersonRequest
    {
        public long Id { get; set; }
        public PersonNameRequest Name { get; set; } = new();
        public Gender Gender { get; set; } = Gender.Other;
        public DateTime? Birthday { get; set; }
        public string FavoriteColor { get; set; } = string.Empty;
        public EmailRequest Email { get; set; } = new();
    }
}
