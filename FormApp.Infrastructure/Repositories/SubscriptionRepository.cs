using FormApp.Core.IRepositories;
using FormApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormApp.Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Core.Entities.Subscription?> GetByIdAsync(Guid id)
    {
        return await _context.Subscriptions.FindAsync(id);
    }

    public async Task<Core.Entities.Subscription> AddAsync(Core.Entities.Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
        await _context.SaveChangesAsync();
        return subscription;
    }

    public async Task<Core.Entities.Subscription> UpdateAsync(Core.Entities.Subscription subscription)
    {
        _context.Subscriptions.Update(subscription);
        await _context.SaveChangesAsync();
        return subscription;
    }
}
