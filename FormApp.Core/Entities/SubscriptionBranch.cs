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
    
    // Branch Meter Information
    public string? BranchMeasurementNumber { get; set; }  // رقم المقياس
    public string? BranchActualMeasurementNumber { get; set; }  // رقم المقياس الفعلي
    public MeasurementUnitActual? BranchMeasurementUnitActual { get; set; }  // وحدات المقياس
    public MeasurementUnitScan? BranchMeasurementUnitScan { get; set; }  // وحدات المقياس
    public string? BranchReadingNumber { get; set; }  // رقم القراءة
    public bool BranchIsNotRealReadingNumber { get; set; }  // هل رقم القراءة حقيقي أم لا
    public string? BranchMultiplicationFactor { get; set; }  // معامل الضرب
    public MeterStatus? BranchMeterStatus { get; set; }  // حالة العداد
    public string? BranchManufacturingCompany { get; set; }  // شركة التصنيع
    public string? BranchMeterNotes { get; set; }  // ملاحظات المقياس
    
    // Foreign Key
    public Guid TransformerId { get; set; }
    public Transformer? Transformer { get; set; }
}
