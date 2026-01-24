using Microsoft.AspNetCore.Http;

namespace FormApp.Helper.FileUtilities;

/// <summary>
/// Provides secure image validation including MIME type and magic bytes (file signature) verification
/// </summary>
public static class ImageValidator
{
    // Whitelisted MIME types for images
    private static readonly HashSet<string> AllowedMimeTypes = new()
    {
        "image/jpeg",
        "image/jpg",
        "image/png",
        "image/gif",
        "image/webp",
        "image/bmp"
    };

    // File extensions to MIME type mapping
    private static readonly Dictionary<string, string> ExtensionToMimeType = new()
    {
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" },
        { ".gif", "image/gif" },
        { ".webp", "image/webp" },
        { ".bmp", "image/bmp" }
    };

    // Magic bytes (file signatures) for image validation
    private static readonly Dictionary<string, List<byte[]>> MagicBytes = new()
    {
        {
            "image/jpeg", new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }, // JPEG
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 }, // JPEG EXIF
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 }, // JPEG Canon
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }, // JPEG Samsung
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }  // JPEG SPIFF
            }
        },
        {
            "image/png", new List<byte[]>
            {
                new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
            }
        },
        {
            "image/gif", new List<byte[]>
            {
                new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 }, // GIF87a
                new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }  // GIF89a
            }
        },
        {
            "image/webp", new List<byte[]>
            {
                new byte[] { 0x52, 0x49, 0x46, 0x46 } // RIFF (WebP starts with RIFF)
            }
        },
        {
            "image/bmp", new List<byte[]>
            {
                new byte[] { 0x42, 0x4D } // BM
            }
        }
    };

    /// <summary>
    /// Validates if the file is a legitimate image by checking MIME type, extension, and magic bytes
    /// </summary>
    public static async Task<(bool IsValid, string ErrorMessage)> ValidateImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return (false, "File is required and cannot be empty");

        // 1. Check file size (max 5MB for images)
        const long maxFileSize = 5 * 1024 * 1024; // 5MB
        if (file.Length > maxFileSize)
            return (false, $"Image size cannot exceed {maxFileSize / (1024 * 1024)}MB");

        // 2. Validate file extension
        var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
        if (string.IsNullOrEmpty(extension) || !ExtensionToMimeType.ContainsKey(extension))
            return (false, $"Invalid image extension. Allowed: {string.Join(", ", ExtensionToMimeType.Keys)}");

        // 3. Validate MIME type from request
        if (string.IsNullOrEmpty(file.ContentType) || !AllowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
            return (false, $"Invalid MIME type. Expected image MIME type, got: {file.ContentType}");

        // 4. Cross-check extension with MIME type
        var expectedMimeType = ExtensionToMimeType[extension];
        if (!file.ContentType.ToLowerInvariant().Equals(expectedMimeType, StringComparison.OrdinalIgnoreCase))
            return (false, $"File extension ({extension}) does not match MIME type ({file.ContentType})");

        // 5. Validate magic bytes (file signature) - most important security check
        var isValidMagicBytes = await ValidateMagicBytesAsync(file, file.ContentType);
        if (!isValidMagicBytes)
            return (false, "File content does not match image format. Possible file spoofing detected.");

        // 6. Validate filename for security (no path traversal)
        if (file.FileName.Contains("..") || file.FileName.Contains("/") || file.FileName.Contains("\\"))
            return (false, "Invalid filename. Path traversal characters detected.");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates the file's magic bytes against known image signatures
    /// </summary>
    private static async Task<bool> ValidateMagicBytesAsync(IFormFile file, string mimeType)
    {
        if (!MagicBytes.TryGetValue(mimeType.ToLowerInvariant(), out var expectedSignatures))
            return false;

        using var stream = file.OpenReadStream();
        var maxSignatureLength = expectedSignatures.Max(s => s.Length);
        var buffer = new byte[maxSignatureLength];
        
        var bytesRead = await stream.ReadAsync(buffer, 0, maxSignatureLength);
        if (bytesRead < expectedSignatures.Min(s => s.Length))
            return false;

        // Reset stream position for later use
        stream.Position = 0;

        // Check if any expected signature matches
        foreach (var signature in expectedSignatures)
        {
            if (buffer.Take(signature.Length).SequenceEqual(signature))
                return true;

            // Special case for WebP - need to check RIFF + WEBP identifier
            if (mimeType.ToLowerInvariant() == "image/webp")
            {
                if (buffer.Take(4).SequenceEqual(new byte[] { 0x52, 0x49, 0x46, 0x46 }))
                {
                    // Check for WEBP identifier at offset 8
                    if (bytesRead >= 12)
                    {
                        var webpIdentifier = new byte[] { 0x57, 0x45, 0x42, 0x50 }; // "WEBP"
                        if (buffer.Skip(8).Take(4).SequenceEqual(webpIdentifier))
                            return true;
                    }
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Generates a secure random filename while preserving the extension
    /// </summary>
    public static string GenerateSecureFileName(string originalExtension)
    {
        // Use Guid + timestamp for uniqueness
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var guid = Guid.NewGuid().ToString("N"); // N format removes hyphens
        return $"{timestamp}_{guid}{originalExtension}";
    }

    /// <summary>
    /// Sanitizes filename by removing any potentially dangerous characters
    /// </summary>
    public static string SanitizeFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return "file";

        // Remove path traversal characters
        fileName = fileName.Replace("..", "").Replace("/", "").Replace("\\", "");
        
        // Remove special characters except dot, dash, underscore
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = new string(fileName.Where(c => !invalidChars.Contains(c)).ToArray());
        
        // Limit length
        if (sanitized.Length > 200)
            sanitized = sanitized.Substring(0, 200);

        return string.IsNullOrWhiteSpace(sanitized) ? "file" : sanitized;
    }
}
