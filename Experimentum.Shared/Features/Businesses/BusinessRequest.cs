using Experimentum.Shared.Features.Emails;

namespace Experimentum.Shared.Features.Businesses
{
    public class BusinessRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public EmailRequest Email { get; set; } = new();
    }
}
