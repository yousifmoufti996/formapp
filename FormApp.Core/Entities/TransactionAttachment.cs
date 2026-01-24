using FormApp.Core.Enums;

namespace FormApp.Core.Entities;

public class TransactionAttachment
{
    public Guid Id { get; set; }
    
    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty;
    public AttachmentType? FileType { get; set; }
    
    public Guid FileId { get; set; }
    public UploadedFile File { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid CreatedById { get; set; }
    public User? CreatedBy { get; set; }
}
