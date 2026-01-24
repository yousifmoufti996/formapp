using Microsoft.AspNetCore.Http;
using FormApp.Core.Entities;

namespace FormApp.Application.Interfaces;

public interface IFileService
{
    Task<UploadedFile> UploadFileAsync(IFormFile file, string uploadPath);
    Task<bool> DeleteFileAsync(string filePath);
    string GetFileUrl(string filePath);
}
