namespace SomeShop.Infrastructure.Repositories;

public abstract class Repository<T>
    where T : class
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Set<T>().FindAsync(id, ct);
    }

    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
}
