using FormApp.Application.DTOs.Common;
using FormApp.Application.DTOs.Transactions;

namespace FormApp.Application.Interfaces;

public interface ITransactionAttachmentService
{
    Task<PagedResultDto<TransactionAttachmentResponseDto>> GetByTransactionRecordAsync(Guid transactionRecordId, PaginationRequestDto? pagination);
    Task<TransactionAttachmentResponseDto> GetByIdAsync(Guid id);
    Task<TransactionAttachmentResponseDto> CreateAsync(AddTransactionAttachmentDto dto, Guid currentUserId);
    Task<IEnumerable<TransactionAttachmentResponseDto>> CreateMultipleImagesAsync(AddMultipleTransactionImagesDto dto, Guid currentUserId);
    Task<TransactionAttachmentResponseDto> UpdateAsync(Guid id, UpdateTransactionAttachmentDto dto, Guid currentUserId);
    Task DeleteAsync(Guid id);
}
