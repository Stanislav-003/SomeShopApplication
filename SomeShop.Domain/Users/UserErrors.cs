using SomeShop.Domain.Abstractions;

namespace SomeShop.Domain.Users;

public static class UserErrors
{
    public static Error InvalidDateRange => new("Error.InvalidDateRange", "Start date cannot be later than end date.");
    public static Error PurchaseCannotBeNull => new("Error.PurchaseCannotBeNull", "Purchase cannot be null.");
    
    public static readonly Error NotFound = new("User.Found", "The user with the specified identifier was not found");
}
