using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;
using System.Linq;

namespace SomeShop.Application.Users.GetUsersForNDays;

public class GetUsersForNDaysQueryHandler : IQueryHandler<GetUsersForNDaysQuery, IEnumerable<GetUsersForNDaysResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public GetUsersForNDaysQueryHandler(IUserRepository userRepository, IPurchaseRepository purchaseRepository)
    {
        _userRepository = userRepository;
        _purchaseRepository = purchaseRepository;
    }

    public async Task<Result<IEnumerable<GetUsersForNDaysResponse>>> Handle(GetUsersForNDaysQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetRecentPurchasesAsync(request.nDays, cancellationToken);

        var result = users.Select(user =>
        {
            var lastPurchase = user.Purchases
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefault();

            return new GetUsersForNDaysResponse(
                user.Id.Value,
                user.FullName.FirstName,
                user.FullName.LastName,
                lastPurchase!.CreatedAt
            );
        }).ToList();

        return result;
    }
}
