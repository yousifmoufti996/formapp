using FormApp.Core.Enums;

namespace FormApp.Core.Entities;

public class Subscriber
{
    public Guid Id { get; set; }
    
    // Basic Information
    public string FacilityName { get; set; } = string.Empty;  // اسم المرافق
    public string SubscriptionNumber { get; set; } = string.Empty;  // رقم الاشتراك
    public string AccountNumber { get; set; } = string.Empty;  // رقم الحساب
    public string Mobile1 { get; set; } = string.Empty;  // رقم الموبايل 1
    public string? Mobile2 { get; set; }  // رقم الموبايل 2
    
    // Address Information
    public AddressStatus AddressStatus { get; set; }  // حالة العنوان
    public AnyAddress AnyAddress { get; set; }  // حالة العنوان
    
    // Neighborhood
    public string? EstimatedNeighborhood { get; set; }  // الحي المقدر
    public string? ActualNeighborhood { get; set; }  // الحي الفعلي
    
    // Geographic Information
    public decimal? Latitude { get; set; }  // خط العرض
    public decimal? Longitude { get; set; }  // خط الطول
    public string? NearestPoint { get; set; }  // أقرب نقطة
    public int? EstimatedAlley { get; set; }  // الزقاق المقدر
    public int? EstimatedHouseNumber { get; set; }  // رقم الدار المقدر
    public int? ActualAlley { get; set; }  // الزقاق الفعلي
    public int? ActualHouseNumber { get; set; }  // رقم الدار الفعلي
    public string? EstimatedDetails { get; set; }  // تفاصيل مقدرة
    public string? ActualDetails { get; set; }  // تفاصيل فعلية
    
    // Additional Information
    public string? CommercialAccountName { get; set; }  // الاسم التجاري المستخدم في الحساب
    public string? FieldPersonName { get; set; }  // اسم الشخص الميداني
    public string? FieldElectricCompanyCompanion { get; set; }  // مرافق شركة الكهرباء الميداني
    
    // Navigation property
    public Transaction? Transaction { get; set; }
}
