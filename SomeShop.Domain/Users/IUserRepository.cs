namespace SomeShop.Domain.Users;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersByBirthday(DateTime? birthDay = null, CancellationToken ct = default);
    
    Task<IEnumerable<User>> GetRecentPurchasesAsync(int nDays, CancellationToken ct = default);

    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);

    void Add(User user);
}
