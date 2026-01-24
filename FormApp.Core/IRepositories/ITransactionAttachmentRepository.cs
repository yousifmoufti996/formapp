using FormApp.Core.Entities;

namespace FormApp.Core.IRepositories;

public interface ITransactionAttachmentRepository
{
    Task<TransactionAttachment?> GetByIdAsync(Guid id);
    Task<IEnumerable<TransactionAttachment>> GetByTransactionIdAsync(Guid transactionId);
    Task<TransactionAttachment> AddAsync(TransactionAttachment attachment);
    Task<TransactionAttachment> UpdateAsync(TransactionAttachment attachment);
    Task DeleteAsync(Guid id);
}
