using Microsoft.AspNetCore.Http;

namespace FormApp.Application.DTOs.Transactions;

public class AddTransactionAttachmentDto
{
    public Guid TransactionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public FormApp.Core.Enums.AttachmentType? FileType { get; set; }
    public IFormFile File { get; set; } = null!;
}

public class TransactionImageDto
{
    public FormApp.Core.Enums.AttachmentType? FileType { get; set; }
    public IFormFile Image { get; set; } = null!;
}

public class AddMultipleTransactionImagesDto
{
    public Guid TransactionId { get; set; }
    public List<TransactionImageDto> Images { get; set; } = new();
}

public class UpdateTransactionAttachmentDto
{
    public string Name { get; set; } = string.Empty;
    public FormApp.Core.Enums.AttachmentType? FileType { get; set; }
    public IFormFile? File { get; set; }
}

public class TransactionAttachmentResponseDto
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public FormApp.Core.Enums.AttachmentType? FileType { get; set; }
    public Guid FileId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
}
