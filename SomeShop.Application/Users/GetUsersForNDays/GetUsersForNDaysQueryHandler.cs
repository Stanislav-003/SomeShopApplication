using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Users.GetUsersForNDays;

public class GetUsersForNDaysQueryHandler : IQueryHandler<GetUsersForNDaysQuery, IReadOnlyCollection<GetUsersForNDaysResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public GetUsersForNDaysQueryHandler(IUserRepository userRepository, IPurchaseRepository purchaseRepository)
    {
        _userRepository = userRepository;
        _purchaseRepository = purchaseRepository;
    }

    public async Task<Result<IReadOnlyCollection<GetUsersForNDaysResponse>>> Handle(GetUsersForNDaysQuery request, CancellationToken cancellationToken)
    {
        DateTime endDate = DateTime.UtcNow;
        DateTime startDate = endDate.AddDays(-request.nDays);

        var users = await _userRepository.GetRecentPurchasesAsync(request.nDays, cancellationToken);

        var result = new List<GetUsersForNDaysResponse>();

        foreach (var user in users)
        {
            var recentPurchases = await _purchaseRepository.GetPurchasesByUserId(user.Id, cancellationToken);

            var lastPurchase = recentPurchases
                .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefault();

            if (lastPurchase != null)
            {
                result.Add(new GetUsersForNDaysResponse(
                    user.Id.Value,
                    user.FullName.FirstName,
                    user.FullName.LastName,
                    lastPurchase.CreatedAt
                ));
            }
        }

        return result;
    }
}
