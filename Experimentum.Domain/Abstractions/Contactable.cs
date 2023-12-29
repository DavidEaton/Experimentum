using CSharpFunctionalExtensions;
using Experimentum.Domain.Features;

namespace Experimentum.Domain.Abstractions
{
    public class Contactable : Entity, IContactable
    {
        public static readonly string RequiredMessage = "Please include all required items.";
        public static readonly string NotFoundMessage = $"Item was not found";
        public static readonly string NonuniqueMessage = $"Item has already been entered; each item must be unique.";

        private readonly List<Phone> phones = new();
        public IReadOnlyList<Phone> Phones => phones.ToList();

        public Contactable(IReadOnlyList<Phone> phones = null)
        {
            if (phones is not null)
                foreach (var phone in phones)
                    AddPhone(phone);
        }

        public void UpdatePhones(IReadOnlyList<Phone> phones)
        {
            var toAdd = phones
                .Where(phone => phone.Id == 0)
                .ToArray();

            var toDelete = Phones
                .Where(phone => !phones.Any(callerPhone => callerPhone.Id == phone.Id))
                .ToArray();

            var toModify = Phones
                .Where(phone => phones.Any(callerPhone => callerPhone.Id == phone.Id))
                .ToArray();

            toModify.ToList()
                .ForEach(phone =>
                {
                    Phone phoneFromCaller = phones.Single(callerPhone => callerPhone.Id == phone.Id);

                    if (phone.Number != phoneFromCaller.Number)
                        phone.SetNumber(phoneFromCaller.Number);

                    if (phone.PhoneType != phoneFromCaller.PhoneType)
                        phone.SetPhoneType(phoneFromCaller.PhoneType);
                });

            toDelete.ToList()
                .ForEach(phone => RemovePhone(phone));

            toAdd.ToList()
                .ForEach(phone =>
                {
                    var result = AddPhone(phone);
                    if (result.IsFailure)
                        throw new Exception(result.Error);
                });
        }


        public Result<Phone> AddPhone(Phone phone)
        {
            if (phone is null)
                return Result.Failure<Phone>(RequiredMessage);

            if (HasPhone(phone))
                return Result.Failure<Phone>(NonuniqueMessage);

            phones.Add(phone);

            return Result.Success(phone);
        }

        public Result<Phone> RemovePhone(Phone phone)
        {
            if (phone is null)
                return Result.Failure<Phone>(RequiredMessage);

            if (!phones.Contains(phone))
                return Result.Failure<Phone>(NotFoundMessage);

            phones.Remove(phone);

            return Result.Success(phone);
        }


        public bool HasPhone(Phone phone)
        {
            return Phones.Any(existingPhone => existingPhone.Number == phone.Number);
        }

    }
}
