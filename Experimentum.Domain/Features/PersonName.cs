using CSharpFunctionalExtensions;

namespace Experimentum.Domain.Features
{
    public class PersonName : ValueObject
    {
        public static readonly int MinimumLength = 1;
        public static readonly int MaximumLength = 255;
        public static readonly string InvalidLengthMessage = $"First and last names must be between {MinimumLength} and {MaximumLength} character(s) in length.";
        public static readonly string RequiredMessage = $"Please enter a valid Name; first and last name are required.";

        private PersonName(string lastName, string firstName, string middleName = null)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = string.IsNullOrWhiteSpace(middleName) ? string.Empty : middleName;
        }

        public string LastName { get; }
        public string FirstName { get; }
        public string MiddleName { get; }

        public static Result<PersonName> Create(string lastName, string firstName, string middleName = null)
        {
            if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<PersonName>(RequiredMessage);

            lastName = (lastName ?? string.Empty).Trim();
            firstName = (firstName ?? string.Empty).Trim();
            middleName = (middleName ?? string.Empty).Trim();

            if (lastName.Length < MinimumLength ||
                lastName.Length > MaximumLength ||
                firstName.Length > MaximumLength ||
                firstName.Length > MaximumLength ||
                middleName?.Length > MaximumLength ||
                middleName?.Length > MaximumLength)
                return Result.Failure<PersonName>(InvalidLengthMessage);

            return Result.Success(new PersonName(lastName, firstName, middleName));
        }

        public Result<PersonName> NewLastName(string newLastName)
        {
            newLastName = (newLastName ?? string.Empty).Trim();

            if (newLastName.Length < MinimumLength ||
                newLastName.Length > MaximumLength)
                return Result.Failure<PersonName>(InvalidLengthMessage);

            return Result.Success(new PersonName(newLastName, FirstName, MiddleName));
        }

        public Result<PersonName> NewFirstName(string newFirstName)
        {
            newFirstName = (newFirstName ?? string.Empty).Trim();

            if (newFirstName.Length < MinimumLength ||
                newFirstName.Length > MaximumLength)
                return Result.Failure<PersonName>(InvalidLengthMessage);

            return Result.Success(new PersonName(LastName, newFirstName, MiddleName));
        }

        public Result<PersonName> NewMiddleName(string newMiddleName)
        {
            newMiddleName = (newMiddleName ?? string.Empty).Trim();

            if (newMiddleName?.Length < MinimumLength ||
                newMiddleName?.Length > MaximumLength)
                return Result.Failure<PersonName>(InvalidLengthMessage);

            return Result.Success(new PersonName(LastName, FirstName, newMiddleName));
        }

        public string LastFirstMiddle
        {
            get => string.IsNullOrWhiteSpace(MiddleName) ? $"{LastName}, {FirstName}" : $"{LastName}, {FirstName} {MiddleName}";
        }
        public string LastFirstMiddleInitial
        {
            get => string.IsNullOrWhiteSpace(MiddleName) ? $"{LastName}, {FirstName}" : $"{LastName}, {FirstName} {MiddleName[0]}.";
        }
        public string FirstMiddleLast
        {
            get => string.IsNullOrWhiteSpace(MiddleName) ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";
        }

        public override string ToString()
        {
            return LastFirstMiddleInitial;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return LastName;
            yield return FirstName;
            yield return MiddleName;
        }

        protected PersonName() { }
    }
}