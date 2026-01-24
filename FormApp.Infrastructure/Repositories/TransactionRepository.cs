using FormApp.Core.Entities;
using FormApp.Core.IRepositories;
using FormApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormApp.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _context.Transactions
            .Include(t => t.Subscriber)
            .Include(t => t.Subscription)
            .Include(t => t.MeterScale)
            .Include(t => t.Violation)
            .Include(t => t.Transformer)
                .ThenInclude(tr => tr.Branches)
            .Include(t => t.Attachments)
                .ThenInclude(a => a.File)
            .Include(t => t.Attachments)
                .ThenInclude(a => a.CreatedBy)
            .Include(t => t.CreatedBy)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Transaction?> GetBySubscriberIdAsync(Guid subscriberId)
    {
        return await _context.Transactions
            .FirstOrDefaultAsync(t => t.SubscriberId == subscriberId);
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transactions
            .Include(t => t.Subscriber)
            .Include(t => t.Subscription)
            .Include(t => t.MeterScale)
            .Include(t => t.Violation)
            .Include(t => t.Transformer)
                .ThenInclude(tr => tr.Branches)
            .Include(t => t.Attachments)
                .ThenInclude(a => a.File)
            .Include(t => t.Attachments)
                .ThenInclude(a => a.CreatedBy)
            .Include(t => t.CreatedBy)
            .ToListAsync();
    }

    public async Task<Transaction> AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> UpdateAsync(Transaction transaction)
    {
        transaction.UpdatedAt = DateTime.UtcNow;
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task DeleteAsync(Guid id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Transactions.AnyAsync(t => t.Id == id);
    }
}
