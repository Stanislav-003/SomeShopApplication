using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Purchases;

namespace SomeShop.Domain.Users;

public class User
{
    private User()
    {
    }
    
    public UserId Id { get; private set; }
    public FullName FullName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public DateTime RegistrationDate { get; private set; }

    private readonly HashSet<Purchase> _purchases = new();
    public IReadOnlyCollection<Purchase> Purchases => _purchases;

    public static Result<User> Create(FullName fullName, DateTime dateOfBirth)
    {
        var user = new User
        {
            Id = new UserId(Guid.NewGuid()),
            FullName = fullName,
            DateOfBirth = dateOfBirth,
            RegistrationDate = DateTime.UtcNow
        };

        return user;
    }

    //public Result AddPurchase(Purchase purchase)
    //{
    //    _purchases.Add(purchase);
    //    return Result.Success();
    //}
}
