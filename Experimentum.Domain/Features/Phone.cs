using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations;
using Entity = Experimentum.Domain.Abstractions.Entity;

namespace Experimentum.Domain.Features
{
    public class Phone : Entity
    {
        public static readonly string InvalidMessage = "Phone number and/or its format is invalid";
        public static readonly string EmptyMessage = "Phone number cannot be empty";
        public static readonly string PhoneTypeInvalidMessage = $"Please enter a valid Phone Type";

        public string Number { get; private set; }
        public PhoneType PhoneType { get; private set; }

        private Phone(string number, PhoneType phoneType)
        {
            Number = number;
            PhoneType = phoneType;
        }

        public static Result<Phone> Create(string number, PhoneType phoneType)
        {
            var errors = new List<string>();

            number = (number ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(number))
                errors.Add(EmptyMessage);

            if (!Enum.IsDefined(typeof(PhoneType), phoneType))
                errors.Add(PhoneTypeInvalidMessage);

            var phoneAttribute = new PhoneAttribute();

            if (!phoneAttribute.IsValid(number))
                errors.Add(InvalidMessage);

            if (errors.Count > 0)
                return Result.Failure<Phone>(string.Join("; ", errors));

            return Result.Success(new Phone(number, phoneType));
        }
    }
}
