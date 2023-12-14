using CSharpFunctionalExtensions;
using Entity = Experimentum.Domain.Abstractions.Entity;

namespace Experimentum.Domain.Features
{
    public class Person : Entity
    {
        public static readonly string RequiredMessage = "Please enter all required items.";
        public static readonly string EmailRequiredMessage = "Please enter a valid email address.";
        public static readonly string NameRequiredMessage = "Please enter a Name.";
        public static readonly string InvalidBirthdayMessage = $"Birth date was invalid";
        public static readonly string InvalidValueMessage = $"Value was invalid";
        public static readonly int FavoriteColorMinimumLength = 3;
        public static readonly int FavoriteColorMaximumLength = 25;
        public static readonly string FavoriteColorMinimumLengthMessage = $"Favorite Color cannot be less than {FavoriteColorMinimumLength} character(s) in length";
        public static readonly string FavoriteColorMaximumLengthMessage = $"Favorite Color cannot be over {FavoriteColorMaximumLength} characters in length";

        public PersonName Name { get; private set; }
        public Gender Gender { get; private set; }
        public DateTime? Birthday { get; private set; }
        public string FavoriteColor { get; private set; }
        public Email Email { get; private set; }

        private readonly List<Phone> phones = new();
        public IReadOnlyList<Phone> Phones => phones.ToList();

        private Person(PersonName name, Gender gender, DateTime? birthday, string favoriteColor, Email email)
        {
            Name = name;
            Gender = gender;
            Birthday = birthday;
            FavoriteColor = favoriteColor;
            Email = email;
        }

        public static Result<Person> Create(PersonName name, Gender gender, DateTime? birthday, string favoriteColor, Email email)
        {
            var errors = new List<string>();

            if (name is null)
                errors.Add(NameRequiredMessage);

            if (!Enum.IsDefined(typeof(Gender), gender))
                errors.Add(RequiredMessage);

            if (birthday.HasValue && !IsValidAgeOn(birthday))
                errors.Add(InvalidBirthdayMessage);

            favoriteColor = (favoriteColor ?? string.Empty).Trim();
            if (favoriteColor.Length < FavoriteColorMinimumLength)
                errors.Add(FavoriteColorMinimumLengthMessage);

            if (favoriteColor.Length > FavoriteColorMaximumLength)
                errors.Add(FavoriteColorMaximumLengthMessage);

            if (email is null)
                errors.Add(EmailRequiredMessage);

            if (errors.Count > 0)
                return Result.Failure<Person>(string.Join("; ", errors));

            return Result.Success(new Person(name, gender, birthday, favoriteColor, email));
        }

        public static bool IsValidAgeOn(DateTime? birthDate)
        {
            if (birthDate is null)
                return false;

            if (!birthDate.HasValue)
                return false;

            if (birthDate >= DateTime.Today)
                return false;

            int thisYear = DateTime.Today.Year;
            int birthYear = birthDate.Value.Year;

            if (birthYear <= thisYear && birthYear > thisYear - 120)
                return true;

            return false;
        }

        public Result<PersonName> SetName(PersonName name)
        {
            if (name is null)
                return Result.Failure<PersonName>(NameRequiredMessage);

            return Result.Success(Name = name);
        }

        public Result<Gender> SetGender(Gender gender)
        {
            if (!Enum.IsDefined(typeof(Gender), gender))
                return Result.Failure<Gender>(RequiredMessage);

            return Result.Success(Gender = gender);
        }

        public Result<DateTime?> SetBirthday(DateTime? birthday)
        {
            if (!IsValidAgeOn(birthday))
                return Result.Failure<DateTime?>(InvalidBirthdayMessage);

            return Result.Success(Birthday = birthday);
        }

        public Result<Email> SetEmail(Email email)
        {
            if (email is null)
                return Result.Failure<Email>(EmailRequiredMessage);

            return Result.Success(Email = email);
        }

        public Result<string> SetFavoriteColor(string favoriteColor)
        {
            var errors = new List<string>();
            favoriteColor = (favoriteColor ?? string.Empty).Trim();

            if (favoriteColor.Length < FavoriteColorMinimumLength)
                errors.Add(FavoriteColorMinimumLengthMessage);

            if (favoriteColor.Length > FavoriteColorMaximumLength)
                errors.Add(FavoriteColorMaximumLengthMessage);

            if (errors.Count > 0)
                return Result.Failure<string>(string.Join("; ", errors));

            FavoriteColor = favoriteColor;
            return Result.Success(FavoriteColor);
        }

        protected Person() { }
    }
}
