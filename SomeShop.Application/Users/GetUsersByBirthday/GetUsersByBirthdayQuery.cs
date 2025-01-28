using SomeShop.Application.Abstractions.Messaging;

namespace SomeShop.Application.Users.GetUsersByBirthday;

public record GetUsersByBirthdayQuery(DateTime? birthday = null) : IQuery<IReadOnlyList<UsersResponse>>;