using Experimentum.Domain.Features;

namespace Experimentum.Shared.Features.Phones
{
    public static class PhoneContractExtensions
    {
        public static PhoneRequest ToRequest(this Phone phone)
        {
            return phone is null
                ? new()
                : new()
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = phone.PhoneType
                };
        }

        public static PhoneRequest ToRequest(this PhoneResponse phone)
        {
            return phone is null
                ? new()
                : new()
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = phone.PhoneType
                };
        }

        public static PhoneResponse ToResponse(this Phone phone) =>
            phone is null
                ? new()
                : new()
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = phone.PhoneType
                };

        public static PhoneResponse ToResponse(this PhoneRequest phone) =>
            phone is null
                ? new()
                : new()
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = phone.PhoneType
                };
    }
}
