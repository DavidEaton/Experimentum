using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Emails;
using Experimentum.Shared.Features.Persons.PersonNames;
using Experimentum.Shared.Features.Phones;

namespace Experimentum.Shared.Features.Persons
{
    public class PersonResponse
    {
        public long Id { get; set; } = default;
        public PersonNameResponse Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string FavoriteColor { get; set; }
        public EmailResponse Email { get; set; }
        public List<PhoneResponse> Phones { get; set; } = new();
    }
}
