using FormApp.Application.DTOs.Transactions;

namespace FormApp.Application.Interfaces;

public interface ITransactionAttachmentService
{
    Task<IEnumerable<TransactionAttachmentResponseDto>> GetByTransactionRecordAsync(Guid transactionRecordId);
    Task<TransactionAttachmentResponseDto> GetByIdAsync(Guid id);
    Task<TransactionAttachmentResponseDto> CreateAsync(AddTransactionAttachmentDto dto, Guid currentUserId);
    Task<IEnumerable<TransactionAttachmentResponseDto>> CreateMultipleImagesAsync(AddMultipleTransactionImagesDto dto, Guid currentUserId);
    Task<TransactionAttachmentResponseDto> UpdateAsync(Guid id, UpdateTransactionAttachmentDto dto, Guid currentUserId);
    Task DeleteAsync(Guid id);
}
