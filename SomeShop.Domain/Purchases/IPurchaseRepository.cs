using SomeShop.Domain.Users;

namespace SomeShop.Domain.Purchases;

public interface IPurchaseRepository
{
    Task<IEnumerable<Purchase>> GetPurchasesByUserId(Guid userId, CancellationToken cancellation = default);
    Task<IEnumerable<Purchase>> GetPurchasesByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    void Add(Purchase purchase);
}
