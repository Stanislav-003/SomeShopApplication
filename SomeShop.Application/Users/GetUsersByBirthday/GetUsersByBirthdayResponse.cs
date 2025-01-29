using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.GetUsersByBirthday;

public record GetUsersByBirthdayResponse(Guid userId, string firstName, string lastName);
