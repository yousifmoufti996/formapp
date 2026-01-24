using Microsoft.AspNetCore.Http;
using FormApp.Core.Entities;

namespace FormApp.Application.Interfaces;

/// <summary>
/// Interface for secure image upload service
/// </summary>
public interface ISecureImageService
{
    /// <summary>
    /// Uploads a single image with comprehensive security validation
    /// </summary>
    Task<UploadedFile> UploadImageAsync(IFormFile file, string uploadPath);
    
    /// <summary>
    /// Uploads a single image with comprehensive security validation and custom filename prefix
    /// </summary>
    Task<UploadedFile> UploadImageAsync(IFormFile file, string uploadPath, string? customPrefix);
    
    /// <summary>
    /// Uploads multiple images with security validation
    /// </summary>
    Task<IEnumerable<UploadedFile>> UploadMultipleImagesAsync(IFormFileCollection files, string uploadPath);
    
    /// <summary>
    /// Deletes an image file securely
    /// </summary>
    Task<bool> DeleteImageAsync(string filePath);
    
    /// <summary>
    /// Gets the URL for accessing an uploaded image
    /// </summary>
    string GetImageUrl(string filePath);
}
