using FormApp.Core.Entities;
using FormApp.Core.IRepositories;
using FormApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormApp.Infrastructure.Repositories;

public class UploadedFileRepository : IUploadedFileRepository
{
    private readonly ApplicationDbContext _context;

    public UploadedFileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UploadedFile?> GetByIdAsync(Guid id)
    {
        return await _context.UploadedFiles.FindAsync(id);
    }

    public async Task<UploadedFile> CreateAsync(UploadedFile file)
    {
        file.Id = Guid.NewGuid();
        file.CreatedAt = DateTime.UtcNow;
        
        _context.UploadedFiles.Add(file);
        await _context.SaveChangesAsync();
        
        return file;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var file = await _context.UploadedFiles.FindAsync(id);
        if (file == null)
            return false;

        _context.UploadedFiles.Remove(file);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
