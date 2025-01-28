using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.GetUsersForNDays;

public class GetUsersForNDaysQueryHandler : IQueryHandler<GetUsersForNDaysQuery, IReadOnlyList<UsersResponse>>
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IUserRepository _userRepository;

    public GetUsersForNDaysQueryHandler(IPurchaseRepository purchaseRepository, IUserRepository userRepository)
    {
        _purchaseRepository = purchaseRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<IReadOnlyList<UsersResponse>>> Handle(GetUsersForNDaysQuery request, CancellationToken cancellationToken)
    {
        DateTime endDate = DateTime.UtcNow;
        DateTime startDate = endDate.AddDays(-request.nDays);

        var recentPurchases = await _purchaseRepository.GetPurchasesForPeriod(startDate, endDate, cancellationToken);

        var usersWithLastPurchase = recentPurchases
            .GroupBy(p => p.UserId)
            .Select(g => new UsersResponse(
                g.Key,
                g.OrderByDescending(p => p.CreatedAt).FirstOrDefault()!.User.FirstName.Value,
                g.OrderByDescending(p => p.CreatedAt).FirstOrDefault()!.User.LastName.Value,
                g.OrderByDescending(p => p.CreatedAt).FirstOrDefault()!.CreatedAt
            ))
            .ToList();

        return usersWithLastPurchase;
    }
}
