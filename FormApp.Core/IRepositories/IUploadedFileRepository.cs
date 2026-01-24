using FormApp.Core.Entities;

namespace FormApp.Core.IRepositories;

public interface IUploadedFileRepository
{
    Task<UploadedFile?> GetByIdAsync(Guid id);
    Task<UploadedFile> CreateAsync(UploadedFile file);
    Task<bool> DeleteAsync(Guid id);
}
