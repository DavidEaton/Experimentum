using CSharpFunctionalExtensions;
using Entity = Experimentum.Domain.Abstractions.Entity;

namespace Experimentum.Domain.Features
{
    public class Business : Entity
    {
        public static readonly string RequiredMessage = "Please enter all required item.";
        public static readonly int MinimumLength = 3;
        public static readonly int MaximumLength = 25;
        public static readonly string InvalidLengthMessage = $"Name must be between {MinimumLength} and {MaximumLength} character(s) in length.";

        public string Name { get; private set; }
        public Email Email { get; private set; }

        private Business(string name, Email email)
        {
            Name = name;
            Email = email;
        }

        public static Result<Business> Create(string name, Email email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Business>(InvalidLengthMessage);
            }

            if (email is null)
            {
                return Result.Failure<Business>(InvalidLengthMessage);
            }

            name = (name ?? string.Empty).Trim();

            if (name.Length < MinimumLength || name.Length > MaximumLength)
            {
                return Result.Failure<Business>(InvalidLengthMessage);
            }

            return Result.Success(new Business(name, email));
        }
    }
}
