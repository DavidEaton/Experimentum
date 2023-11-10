using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Experimentum.Domain.Features
{
    public class Email : ValueObject
    {
        public string Address { get; private set; }

        private Email(string address)
        {
            Address = address;
        }

        public static Result<Email> Create(string address) => Regex.IsMatch(address,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase)
                ? Result.Success(new Email(address))
                : Result.Failure<Email>("Invalid email address");

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Address;
        }
        protected Email() { }
    }
}
