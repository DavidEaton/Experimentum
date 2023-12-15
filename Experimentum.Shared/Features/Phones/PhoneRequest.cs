using Experimentum.Domain.Features;

namespace Experimentum.Shared.Features.Phones
{
    public class PhoneRequest
    {
        public long Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public PhoneType PhoneType { get; set; } = PhoneType.Other;
    }
}
