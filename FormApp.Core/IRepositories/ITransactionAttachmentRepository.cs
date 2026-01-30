using FormApp.Core.Entities;

namespace FormApp.Core.IRepositories;

public interface ITransactionAttachmentRepository
{
    Task<TransactionAttachment?> GetByIdAsync(Guid id);
    Task<IEnumerable<TransactionAttachment>> GetByTransactionIdAsync(Guid transactionId);
    Task<(IEnumerable<TransactionAttachment> items, int totalCount)> GetByTransactionIdPagedAsync(Guid transactionId, int pageNumber, int pageSize);
    Task<TransactionAttachment> AddAsync(TransactionAttachment attachment);
    Task<TransactionAttachment> UpdateAsync(TransactionAttachment attachment);
    Task DeleteAsync(Guid id);
}
