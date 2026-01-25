using FormApp.Application.DTOs.Transactions;

namespace FormApp.Application.Interfaces;

public interface ITransactionRecordService
{
    Task<IEnumerable<TransactionRecordResponseDto>> GetAllAsync(Guid userId);
    Task<TransactionRecordResponseDto> GetByIdAsync(Guid id, Guid userId);
    Task<TransactionRecordResponseDto> CreateAsync(CreateTransactionRecordDto dto, Guid currentUserId);
    Task<TransactionRecordResponseDto> UpdateAsync(Guid id, CreateTransactionRecordDto dto);
    Task DeleteAsync(Guid id);
}
