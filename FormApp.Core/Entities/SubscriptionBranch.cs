using FormApp.Core.Enums;

namespace FormApp.Core.Entities;

public class SubscriptionBranch
{
    public Guid Id { get; set; }
    
    // Branch Information
    public BranchType? BranchType { get; set; }  // نوع الفرع
    public int BranchCount { get; set; }  // عدد الفروع
    public string? Name { get; set; }  // اسم الفرع
    public string? Size { get; set; }  // حجم الفرع
    public string? Notes { get; set; }  // ملاحظات
    
    // Foreign Key
    public Guid TransformerId { get; set; }
    public Transformer? Transformer { get; set; }
}
