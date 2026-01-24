using FormApp.Core.Entities;
using FormApp.Core.IRepositories;
using FormApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormApp.Infrastructure.Repositories;

public class TransactionRecordRepository : ITransactionRecordRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionRecord?> GetByIdAsync(Guid id)
    {
        return await _context.TransactionRecords
            .Include(f => f.CreatedBy)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<TransactionRecord>> GetAllAsync()
    {
        return await _context.TransactionRecords
            .Include(f => f.CreatedBy)
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync();
    }

    public async Task<TransactionRecord> CreateAsync(TransactionRecord TransactionRecord)
    {
        TransactionRecord.Id = Guid.NewGuid();
        TransactionRecord.CreatedAt = DateTime.UtcNow;
        
        _context.TransactionRecords.Add(TransactionRecord);
        await _context.SaveChangesAsync();
        
        return await GetByIdAsync(TransactionRecord.Id) ?? TransactionRecord;
    }

    public async Task<TransactionRecord> UpdateAsync(TransactionRecord TransactionRecord)
    {
        TransactionRecord.UpdatedAt = DateTime.UtcNow;
        
        _context.TransactionRecords.Update(TransactionRecord);
        await _context.SaveChangesAsync();
        
        return await GetByIdAsync(TransactionRecord.Id) ?? TransactionRecord;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var TransactionRecord = await _context.TransactionRecords.FindAsync(id);
        if (TransactionRecord == null)
            return false;

        _context.TransactionRecords.Remove(TransactionRecord);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.TransactionRecords.AnyAsync(f => f.Id == id);
    }
}
