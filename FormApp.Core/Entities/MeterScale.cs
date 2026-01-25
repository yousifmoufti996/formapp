using FormApp.Core.Enums;

namespace FormApp.Core.Entities;

public class MeterScale
{
    public Guid Id { get; set; }
    
    // Measurement Information
    public string MeasurementNumber { get; set; } = string.Empty;  // رقم المقياس
    public string ActualMeasurementNumber { get; set; } = string.Empty;  // رقم المقياس الفعلي
    public MeasurementUnitActual? MeasurementUnitActual { get; set; }  // وحدات المقياس
    public MeasurementUnitScan? MeasurementUnitScan { get; set; }  // وحدات المقياس
    public string ReadingNumber { get; set; } = string.Empty;  // رقم القراءة
    public bool IsNotRealReadingNumber { get; set; }  // هل رقم القراءة حقيقي أم لا
    
    public string MultiplicationFactor { get; set; } = string.Empty;  // معامل الضرب
    public MeterStatus MeterStatus { get; set; }  // حالة العداد
    public string? ManufacturingCompany { get; set; }  // شركة التصنيع
    public string? MeterNotes { get; set; }  // ملاحظات المقياس
    
    // Navigation property
    public Transaction? Transaction { get; set; }
}
