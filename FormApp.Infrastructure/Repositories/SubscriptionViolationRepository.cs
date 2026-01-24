using FormApp.Core.Entities;
using FormApp.Core.IRepositories;
using FormApp.Infrastructure.Data;

namespace FormApp.Infrastructure.Repositories;

public class SubscriptionViolationRepository : ISubscriptionViolationRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionViolationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SubscriptionViolation?> GetByIdAsync(Guid id)
    {
        return await _context.SubscriptionViolations.FindAsync(id);
    }

    public async Task<SubscriptionViolation> AddAsync(SubscriptionViolation violation)
    {
        await _context.SubscriptionViolations.AddAsync(violation);
        await _context.SaveChangesAsync();
        return violation;
    }

    public async Task<SubscriptionViolation> UpdateAsync(SubscriptionViolation violation)
    {
        _context.SubscriptionViolations.Update(violation);
        await _context.SaveChangesAsync();
        return violation;
    }
}
