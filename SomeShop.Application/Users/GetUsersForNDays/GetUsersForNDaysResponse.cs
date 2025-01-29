namespace SomeShop.Application.Users.GetUsersForNDays;

public record GetUsersForNDaysResponse(Guid userId, string firstName, string lastName, DateTime lastPurchaseDate);
