using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Application.Users.GetUsersByBirthday;

namespace SomeShop.Application.Users.GetUsersForNDays;

public record GetUsersForNDaysQuery(int nDays) : IQuery<IReadOnlyCollection<GetUsersForNDaysResponse>>;