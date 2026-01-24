using FormApp.Core.Entities;
using FormApp.Core.IRepositories;
using FormApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormApp.Infrastructure.Repositories;

public class SubscriberRepository : ISubscriberRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Subscriber?> GetByIdAsync(Guid id)
    {
        return await _context.Subscribers
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Subscriber?> GetByAccountNumberAsync(string accountNumber)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            return null;
            
        return await _context.Subscribers
            .FirstOrDefaultAsync(s => s.AccountNumber == accountNumber);
    }

    public async Task<Subscriber?> GetBySubscriptionNumberAsync(string subscriptionNumber)
    {
        if (string.IsNullOrWhiteSpace(subscriptionNumber))
            return null;
            
        return await _context.Subscribers
            .FirstOrDefaultAsync(s => s.SubscriptionNumber == subscriptionNumber);
    }

    public async Task<Subscriber> AddAsync(Subscriber subscriber)
    {
        await _context.Subscribers.AddAsync(subscriber);
        await _context.SaveChangesAsync();
        return subscriber;
    }

    public async Task<Subscriber> UpdateAsync(Subscriber subscriber)
    {
        _context.Subscribers.Update(subscriber);
        await _context.SaveChangesAsync();
        return subscriber;
    }
}
