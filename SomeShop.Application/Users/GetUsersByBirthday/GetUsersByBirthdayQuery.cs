using SomeShop.Application.Abstractions.Messaging;

namespace SomeShop.Application.Users.GetUsersByBirthday;

public record GetUsersByBirthdayQuery() : IQuery<IEnumerable<GetUsersByBirthdayResponse>>;