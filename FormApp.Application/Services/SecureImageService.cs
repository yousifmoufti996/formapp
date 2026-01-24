using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FormApp.Application.Interfaces;
using FormApp.Core.Entities;
using FormApp.Core.Exceptions;
using FormApp.Helper.FileUtilities;

namespace FormApp.Application.Services;

/// <summary>
/// Secure image upload service with comprehensive validation
/// </summary>
public class SecureImageService : ISecureImageService
{
    private readonly string _uploadBasePath;
    private readonly ILogger<SecureImageService> _logger;
    private const long MaxImageSize = 5 * 1024 * 1024; // 5MB
    private const int MaxImageWidth = 4096;
    private const int MaxImageHeight = 4096;

    public SecureImageService(string uploadBasePath, ILogger<SecureImageService> logger)
    {
        _uploadBasePath = uploadBasePath;
        _logger = logger;
        
        // Ensure upload directory exists
        if (!Directory.Exists(_uploadBasePath))
        {
            Directory.CreateDirectory(_uploadBasePath);
        }
    }

    public async Task<UploadedFile> UploadImageAsync(IFormFile file, string uploadPath)
    {
        return await UploadImageAsync(file, uploadPath, null);
    }

    public async Task<UploadedFile> UploadImageAsync(IFormFile file, string uploadPath, string? customPrefix)
    {
        // 1. Comprehensive image validation (MIME type + magic bytes)
        var (isValid, errorMessage) = await ImageValidator.ValidateImageAsync(file);
        if (!isValid)
        {
            _logger.LogWarning("Image validation failed: {ErrorMessage}. File: {FileName}, ContentType: {ContentType}, Size: {Size}", 
                errorMessage, file.FileName, file.ContentType, file.Length);
            throw new BadRequestException(errorMessage);
        }

        // 2. Additional security checks
        if (file.Length > MaxImageSize)
            throw new BadRequestException($"Image size cannot exceed {MaxImageSize / (1024 * 1024)}MB");

        // 3. Validate and sanitize upload path (prevent directory traversal)
        uploadPath = SanitizeUploadPath(uploadPath);
        
        // Create upload directory if it doesn't exist
        var fullUploadPath = Path.Combine(_uploadBasePath, uploadPath);
        if (!Directory.Exists(fullUploadPath))
        {
            Directory.CreateDirectory(fullUploadPath);
        }

        // 4. Generate secure filename with optional custom prefix
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        string secureFileName;
        
        if (string.IsNullOrWhiteSpace(customPrefix))
        {
            secureFileName = ImageValidator.GenerateSecureFileName(extension);
        }
        else
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var guid = Guid.NewGuid().ToString("N");
            var sanitizedPrefix = ImageValidator.SanitizeFileName(customPrefix);
            secureFileName = $"{timestamp}_{sanitizedPrefix}_{guid}{extension}";
        }
        
        var filePath = Path.Combine(uploadPath, secureFileName);
        var fullFilePath = Path.Combine(_uploadBasePath, filePath);

        try
        {
            // 5. Save file securely
            using (var stream = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await file.CopyToAsync(stream);
                await stream.FlushAsync();
            }

            // 6. Verify file was written correctly
            if (!File.Exists(fullFilePath))
                throw new Exception("File save verification failed");

            // 7. Optional: Validate image dimensions (prevent resource exhaustion attacks)
            // This requires System.Drawing or ImageSharp library
            // Commented out for now - add if needed:
            // await ValidateImageDimensionsAsync(fullFilePath);

            _logger.LogInformation("Image uploaded successfully: {FileName} -> {SecureFileName}", 
                file.FileName, secureFileName);

            return new UploadedFile
            {
                FileName = secureFileName,
                OriginalFileName = ImageValidator.SanitizeFileName(Path.GetFileName(file.FileName)),
                FileExtension = extension,
                FilePath = filePath.Replace("\\", "/"),
                ContentType = file.ContentType,
                FileSize = file.Length
            };
        }
        catch (Exception ex)
        {
            // Clean up file if save failed
            if (File.Exists(fullFilePath))
            {
                try { File.Delete(fullFilePath); } catch { }
            }

            _logger.LogError(ex, "Failed to upload image: {FileName}", file.FileName);
            throw new Exception("Failed to upload image. Please try again.", ex);
        }
    }

    public async Task<IEnumerable<UploadedFile>> UploadMultipleImagesAsync(IFormFileCollection files, string uploadPath)
    {
        if (files == null || files.Count == 0)
            throw new BadRequestException("At least one image is required");

        if (files.Count > 10)
            throw new BadRequestException("Cannot upload more than 10 images at once");

        var uploadedFiles = new List<UploadedFile>();
        var failedFiles = new List<string>();

        foreach (var file in files)
        {
            try
            {
                var uploadedFile = await UploadImageAsync(file, uploadPath);
                uploadedFiles.Add(uploadedFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload image: {FileName}", file.FileName);
                failedFiles.Add(file.FileName);
            }
        }

        if (failedFiles.Any())
        {
            throw new BadRequestException($"Failed to upload the following images: {string.Join(", ", failedFiles)}");
        }

        return uploadedFiles;
    }

    public Task<bool> DeleteImageAsync(string filePath)
    {
        try
        {
            // Sanitize path to prevent directory traversal
            filePath = SanitizeUploadPath(filePath);
            
            var fullPath = Path.Combine(_uploadBasePath, filePath);
            
            // Security check: ensure the file is within the upload directory
            var fullUploadPath = Path.GetFullPath(_uploadBasePath);
            var fullFilePath = Path.GetFullPath(fullPath);
            
            if (!fullFilePath.StartsWith(fullUploadPath))
            {
                _logger.LogWarning("Attempted to delete file outside upload directory: {FilePath}", filePath);
                return Task.FromResult(false);
            }

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                _logger.LogInformation("Image deleted: {FilePath}", filePath);
                return Task.FromResult(true);
            }
            
            return Task.FromResult(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete image: {FilePath}", filePath);
            return Task.FromResult(false);
        }
    }

    public string GetImageUrl(string filePath)
    {
        // Sanitize and normalize path
        filePath = filePath?.Replace("\\", "/") ?? string.Empty;
        return $"/uploads/{filePath}";
    }

    /// <summary>
    /// Sanitizes upload path to prevent directory traversal attacks
    /// </summary>
    private string SanitizeUploadPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return "default";

        // Remove any path traversal attempts
        path = path.Replace("..", "")
                   .Replace("~", "")
                   .Replace("//", "/")
                   .Replace("\\\\", "\\")
                   .Trim('/', '\\');

        // Remove any absolute path indicators
        if (Path.IsPathRooted(path))
            path = Path.GetFileName(path);

        return string.IsNullOrWhiteSpace(path) ? "default" : path;
    }

    /// <summary>
    /// Optional: Validate image dimensions to prevent resource exhaustion
    /// Requires System.Drawing.Common or ImageSharp NuGet package
    /// </summary>
    private async Task ValidateImageDimensionsAsync(string filePath)
    {
        // TODO: Implement using ImageSharp or System.Drawing
        // Example with ImageSharp:
        // using var image = await Image.LoadAsync(filePath);
        // if (image.Width > MaxImageWidth || image.Height > MaxImageHeight)
        //     throw new BadRequestException($"Image dimensions cannot exceed {MaxImageWidth}x{MaxImageHeight}");
        
        await Task.CompletedTask;
    }
}
