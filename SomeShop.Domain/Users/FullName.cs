using SomeShop.Domain.Abstractions;

namespace SomeShop.Domain.Users;

public class FullName
{
    private FullName()
    {
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public static Result<FullName> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<FullName>(new Error("Firstname.Empty" , "First name cannot be empty."));

        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<FullName>(new Error("Lastname.Empty", "Last name cannot be empty."));

        var fullName = new FullName
        {
            FirstName = firstName,
            LastName = lastName
        };

        return fullName;
    }
}
