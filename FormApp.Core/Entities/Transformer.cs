namespace FormApp.Core.Entities;

public class Transformer
{
    public Guid Id { get; set; }
    
    // Transformer Information
    public Enums.TransformerCapacity? TransformerCapacity { get; set; }  // سعة المحول
    public string? TransformerSerialNumber { get; set; }  // الرقم التسلسلي للمحول
    public string? ManufacturingCompany { get; set; }  // شركة التصنيع
    public bool IsTransformerWorking { get; set; }  // هل المحول يعمل أم لا
    public bool HasBranches { get; set; }  // هل توجد فروع
    
    // Room Information
    public bool HasRoomButtons { get; set; }  // هل تحتوي الغرفة على أزرار
    public bool IsRoomSuitable { get; set; }  // هل الغرفة مناسبة للاستخدام
    public bool CanAddPartitions { get; set; }  // هل يمكننا إضافة فواصل للغرفة
    public string? TransformerNotes { get; set; }  // ملاحظات المحول
    
    // Navigation properties
    public Transaction? Transaction { get; set; }
    public ICollection<SubscriptionBranch> Branches { get; set; } = new List<SubscriptionBranch>();
}
