namespace Experimentum.Shared.Features.Emails
{
    public class EmailResponse
    {
        public string Address { get; set; }
        public string DisplayName => Address;
    }
}
