namespace SomeShop.Application.Users.GetUsersForNDays;

public record UsersResponse(Guid userId, string firstName, string lastName, DateTime lastPurchaseDate);
