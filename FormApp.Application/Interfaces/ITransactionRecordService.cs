using FormApp.Application.DTOs.Common;
using FormApp.Application.DTOs.Transactions;

namespace FormApp.Application.Interfaces;

public interface ITransactionRecordService
{
    Task<PagedResultDto<TransactionRecordResponseDto>> GetAllAsync(PaginationRequestDto? pagination, Guid? userId = null);
    Task<TransactionRecordResponseDto> GetByIdAsync(Guid id, Guid? userId = null);
    Task<TransactionRecordResponseDto> CreateAsync(CreateTransactionRecordDto dto, Guid currentUserId);
    Task<TransactionRecordResponseDto> UpdateAsync(Guid id, CreateTransactionRecordDto dto);
    Task DeleteAsync(Guid id);
}
