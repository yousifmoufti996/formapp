using Microsoft.AspNetCore.Http;
using FormApp.Application.Interfaces;
using FormApp.Core.Entities;
using FormApp.Core.Exceptions;

namespace FormApp.Application.Services;

public class FileService : IFileService
{
    private readonly string _uploadBasePath;
    private readonly long _maxFileSize = 10 * 1024 * 1024; // 10MB
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx", ".xls", ".xlsx" };

    public FileService(string uploadBasePath)
    {
        _uploadBasePath = uploadBasePath;
        
        // Ensure upload directory exists
        if (!Directory.Exists(_uploadBasePath))
        {
            Directory.CreateDirectory(_uploadBasePath);
        }
    }

    public async Task<UploadedFile> UploadFileAsync(IFormFile file, string uploadPath)
    {
        if (file == null || file.Length == 0)
            throw new BadRequestException("File is required");

        if (file.Length > _maxFileSize)
            throw new BadRequestException($"File size cannot exceed {_maxFileSize / (1024 * 1024)}MB");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(extension))
            throw new BadRequestException($"File type {extension} is not allowed");

        // Create upload directory if it doesn't exist
        var fullUploadPath = Path.Combine(_uploadBasePath, uploadPath);
        if (!Directory.Exists(fullUploadPath))
        {
            Directory.CreateDirectory(fullUploadPath);
        }

        // Generate unique filename
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadPath, fileName);
        var fullFilePath = Path.Combine(_uploadBasePath, filePath);

        // Save file
        using (var stream = new FileStream(fullFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return new UploadedFile
        {
            FileName = fileName,
            OriginalFileName = file.FileName,
            FileExtension = extension,
            FilePath = filePath.Replace("\\", "/"),
            ContentType = file.ContentType,
            FileSize = file.Length
        };
    }

    public Task<bool> DeleteFileAsync(string filePath)
    {
        try
        {
            var fullPath = Path.Combine(_uploadBasePath, filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public string GetFileUrl(string filePath)
    {
        return $"/uploads/{filePath.Replace("\\", "/")}";
    }
}
