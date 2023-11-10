using CSharpFunctionalExtensions;
using Entity = Experimentum.Domain.Abstractions.Entity;

namespace Experimentum.Domain.Features
{
    public class Person : Entity
    {
        public static readonly string RequiredMessage = "Please include all required items.";
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
            if (name is null)
                return Result.Failure<Person>(RequiredMessage);

            if (!Enum.IsDefined(typeof(Gender), gender))
                return Result.Failure<Person>(InvalidValueMessage);

            if (birthday.HasValue)
                if (!IsValidAge(birthday))
                    return Result.Failure<Person>(InvalidValueMessage);

            favoriteColor = (favoriteColor ?? string.Empty).Trim();

            if (favoriteColor.Length < FavoriteColorMinimumLength)
                return Result.Failure<Person>(FavoriteColorMinimumLengthMessage);

            if (favoriteColor.Length > FavoriteColorMaximumLength)
                return Result.Failure<Person>(FavoriteColorMinimumLengthMessage);

            if (email is null)
                return Result.Failure<Person>(RequiredMessage);

            return Result.Success(new Person(name, gender, birthday, favoriteColor, email));

        }
        protected static bool IsValidAge(DateTime? birthDate)
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
                return Result.Failure<PersonName>(RequiredMessage);

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
            if (!IsValidAge(birthday))
                return Result.Failure<DateTime?>(InvalidValueMessage);

            return Result.Success(Birthday = birthday);
        }

        public Result<Email> SetEmail(Email email)
        {
            if (email is null)
                return Result.Failure<Email>(RequiredMessage);

            return Result.Success(Email = email);
        }

        public Result<string> SetFavoriteColor(string favoriteColor)
        {
            favoriteColor = (favoriteColor ?? string.Empty).Trim();

            if (favoriteColor.Length < FavoriteColorMinimumLength)
                return Result.Failure<string>(FavoriteColorMinimumLengthMessage);

            if (favoriteColor.Length > FavoriteColorMaximumLength)
                return Result.Failure<string>(FavoriteColorMinimumLengthMessage);

            return Result.Success(FavoriteColor = favoriteColor);
        }

        protected Person() { }
    }
}
