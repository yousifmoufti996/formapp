using FormApp.Core.Entities;

namespace FormApp.Core.IRepositories;

public interface ITransactionRecordRepository
{
    Task<TransactionRecord?> GetByIdAsync(Guid id);
    Task<IEnumerable<TransactionRecord>> GetAllAsync();
    Task<TransactionRecord> CreateAsync(TransactionRecord TransactionRecord);
    Task<TransactionRecord> UpdateAsync(TransactionRecord TransactionRecord);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
