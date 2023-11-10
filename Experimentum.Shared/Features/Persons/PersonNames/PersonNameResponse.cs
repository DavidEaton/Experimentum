namespace Experimentum.Shared.Features.Persons.PersonNames
{
    public class PersonNameResponse
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; } = string.Empty;
        public string DisplayName => $"{LastName}, {FirstName} {MiddleName}";
    }
}
