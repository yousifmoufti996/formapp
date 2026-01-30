using FormApp.Core.Entities;
using FormApp.Core.IRepositories;
using FormApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormApp.Infrastructure.Repositories;

public class TransactionAttachmentRepository : ITransactionAttachmentRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionAttachmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionAttachment?> GetByIdAsync(Guid id)
    {
        return await _context.TransactionAttachments
            .Include(a => a.File)
            .Include(a => a.CreatedBy)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<TransactionAttachment>> GetByTransactionIdAsync(Guid transactionId)
    {
        return await _context.TransactionAttachments
            .Include(a => a.File)
            .Include(a => a.CreatedBy)
            .Where(a => a.TransactionId == transactionId)
            .ToListAsync();
    }

    public async Task<(IEnumerable<TransactionAttachment> items, int totalCount)> GetByTransactionIdPagedAsync(Guid transactionId, int pageNumber, int pageSize)
    {
        var query = _context.TransactionAttachments
            .Include(a => a.File)
            .Include(a => a.CreatedBy)
            .Where(a => a.TransactionId == transactionId)
            .OrderByDescending(a => a.CreatedAt);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<TransactionAttachment> AddAsync(TransactionAttachment attachment)
    {
        await _context.TransactionAttachments.AddAsync(attachment);
        await _context.SaveChangesAsync();
        return attachment;
    }

    public async Task<TransactionAttachment> UpdateAsync(TransactionAttachment attachment)
    {
        attachment.UpdatedAt = DateTime.UtcNow;
        _context.TransactionAttachments.Update(attachment);
        await _context.SaveChangesAsync();
        return attachment;
    }

    public async Task DeleteAsync(Guid id)
    {
        var attachment = await _context.TransactionAttachments.FindAsync(id);
        if (attachment != null)
        {
            _context.TransactionAttachments.Remove(attachment);
            await _context.SaveChangesAsync();
        }
    }
}
