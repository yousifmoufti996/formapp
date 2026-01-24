namespace FormApp.Core.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    
    // Foreign Keys to related entities (One-to-One)
    public Guid SubscriberId { get; set; }
    public Subscriber? Subscriber { get; set; }
    
    public Guid SubscriptionId { get; set; }
    public Subscription? Subscription { get; set; }
    
    public Guid MeterScaleId { get; set; }
    public MeterScale? MeterScale { get; set; }
    
    public Guid ViolationId { get; set; }
    public SubscriptionViolation? Violation { get; set; }
    
    public Guid TransformerId { get; set; }
    public Transformer? Transformer { get; set; }
    
    // Notes
    public string? Notes { get; set; }  // ملاحظات عمومية
    
    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    
    // Navigation properties (One-to-Many)
    public ICollection<TransactionAttachment> Attachments { get; set; } = new List<TransactionAttachment>();
}
