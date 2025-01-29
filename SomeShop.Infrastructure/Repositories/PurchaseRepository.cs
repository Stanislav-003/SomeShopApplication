﻿using Microsoft.EntityFrameworkCore;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;
using System.Threading;

namespace SomeShop.Infrastructure.Repositories;

public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<Purchase>> GetPurchasesByUserId(UserId userId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Purchase>()
            .Where(p => p.UserId == userId) 
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Purchase>> GetPurchasesByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Purchase>()
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .ToListAsync(cancellationToken);
    }
}
