using FormApp.Core.Entities;

namespace FormApp.Core.IRepositories;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<Transaction?> GetByIdAsync(Guid id, Guid userId);
    Task<Transaction?> GetBySubscriberIdAsync(Guid subscriberId);
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<IEnumerable<Transaction>> GetAllAsync(Guid userId);
    Task<Transaction> AddAsync(Transaction transaction);
    Task<Transaction> UpdateAsync(Transaction transaction);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
