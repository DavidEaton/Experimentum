using Experimentum.Domain.Features;

namespace Experimentum.Shared.Features.Phones
{
    public class PhoneResponse
    {
        public long Id { get; set; }
        public string? Number { get; set; }
        public PhoneType PhoneType { get; set; }
    }
}
