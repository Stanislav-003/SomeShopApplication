using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.GetUsersByBirthday;

public record UsersResponse(Guid userId, string firstName, string lastName);
