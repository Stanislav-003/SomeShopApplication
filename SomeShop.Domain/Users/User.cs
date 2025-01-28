using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Purchases;

namespace SomeShop.Domain.Users;

public class User : Entity
{
    private User(Guid id, FirstName firstName, LastName lastName, DateTime dateOfBirth) : base(id)
    {  
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        CreatedAt = DateTime.UtcNow;
    }

    private User()
    {
        
    }

    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }

    private readonly List<Purchase> _purchases = new();
    public IReadOnlyCollection<Purchase> Purchases => _purchases.AsReadOnly();

    public static User Create(FirstName firstName, LastName lastName, DateTime dateOfBirth)
    {
        var user = new User(Guid.NewGuid(), firstName, lastName, dateOfBirth);

        return user;
    }

    public Result AddPurchase(Purchase purchase)
    {
        _purchases.Add(purchase);
        return Result.Success();
    }
}
