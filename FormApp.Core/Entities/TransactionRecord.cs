using FormApp.Core.Enums;

namespace FormApp.Core.Entities;

public class TransactionRecord
{
    public Guid Id { get; set; }
    
    // Basic Information
    public string FacilityName { get; set; } = string.Empty;  // اسم المرافق
    public string SubscriptionNumber { get; set; } = string.Empty;  // رقم الاشتراك
    public string AccountNumber { get; set; } = string.Empty;  // رقم الحساب
    public string Mobile1 { get; set; } = string.Empty;  // رقم الموبايل 1
    public string? Mobile2 { get; set; }  // رقم الموبايل 2
    
    // Subscriber Information
    public string SubscriberName { get; set; } = string.Empty;  // اسم المشترك
    public SubscriptionType SubscriptionType { get; set; }  // نوع الاشتراك
    
    // Facility Classification
    public SubscriptionMenuType ListBuildingType { get; set; }  // صنف الاشتراك حسب القائمة
    public SubscriptionScanType ActualBuildingType { get; set; }  // صنف الاشتراك حسب المسح
    public BeneficiaryStatus BeneficiaryStatus { get; set; }  // حالة المنتفع من الاشتراك
    
    // Measurement Information
    public string MeasurementNumber { get; set; } = string.Empty;  // رقم المقياس
    public string ActualMeasurementNumber { get; set; } = string.Empty;  // رقم المقياس الفعلي
    public MeasurementUnitActual? MeasurementUnitActual { get; set; }  // وحدات المقياس
    public MeasurementUnitScan? MeasurementUnitScan { get; set; }  // وحدات المقياس
    public string ReadingNumber { get; set; } = string.Empty;  // رقم القراءة
    public bool IsNotRealReadingNumber { get; set; }  // هل رقم القراءة حقيقي أم لا

    public string MultiplicationFactor { get; set; } = string.Empty;  // معامل الضرب
    public MeterStatus MeterStatus { get; set; }  // حالة العداد
    
    // Title Information
    public AddressStatus AddressStatus { get; set; }  // حالة العنوان
    public AnyAddress AnyAddress { get; set; }  // حالة العنوان
    
    // Additional Options
    public bool NoMatchingList { get; set; }  // لا يوجد تجاوز فني
    public bool NoMatchingMeasurement { get; set; }  // اشتراك له قائمة وليس له مقياس
    public bool HasDestructionOrDamage { get; set; }  // يوجد مقياس تم تخريبه أو فقده
    public bool IsSharedMeter { get; set; }  // مشترك لديه وصلة خارج المقياس
    public bool HasUnitsFromOtherSubscriptions { get; set; }  // مشترك مع وصلات بعد المقياس لوحدات أخرى
    public bool HasUnitsBeforeMeter { get; set; }  // مشترك لديه وصلات قبل المقياس لوحدات أخرى
    public bool Other { get; set; }  //  أخرى
    
    // Units Count
    public int BenefitingUnitsCount { get; set; }  // عدد الوحدات المستفيدة من الاشتراك
    public int OtherUnitsBeforeMeterCount { get; set; }  // عدد الوحدات المستفيدة من الاشتراك
    
    // Transformer Information
    public string? TransformerCapacity { get; set; }  // سعة المحول
    public string? TransformerSerialNumber { get; set; }  // الرقم التسلسلي للمحول
    public string? ManufacturingCompany { get; set; }  // شركة التصنيع
    public bool IsTransformerWorking { get; set; }  // هل المحول يعمل أم لا
    
    // Geographic Information
    public decimal? Latitude { get; set; }  // خط العرض
    public decimal? Longitude { get; set; }  // خط الطول
    public string? NearestPoint { get; set; }  // أقرب نقطة
    public string? EstimatedNeighborhood { get; set; }  // الحي المقدر
    public string? ActualNeighborhood { get; set; }  // الحي الفعلي
    public int? EstimatedAlley { get; set; }  // الزقاق المقدر
    public int? EstimatedHouseNumber { get; set; }  // رقم الدار المقدر
    public int? ActualAlley { get; set; }  // الزقاق الفعلي
    public int? ActualHouseNumber { get; set; }  // رقم الدار الفعلي
    public string? EstimatedDetails { get; set; }  // تفاصيل مقدرة
    public string? ActualDetails { get; set; }  // تفاصيل فعلية
    
    // Branch Information
    public BranchType? BranchType { get; set; }  // نوع الفرع
    public int BranchCount { get; set; }  // عدد الفروع
    public bool HasBranches { get; set; }  // هل توجد فروع
    
    // Room Information
    public bool HasRoomButtons { get; set; }  // هل تحتوي الغرفة على أزرار
    public bool IsRoomSuitable { get; set; }  // هل الغرفة مناسبة للاستخدام
    public bool CanAddPartitions { get; set; }  // هل يمكننا إضافة فواصل للغرفة
    
    // Additional Information
    public string? CommercialAccountName { get; set; }  // الاسم التجاري المستخدم في الحساب
    public string? FieldPersonName { get; set; }  // اسم الشخص الميداني
    public string? FieldElectricCompanyCompanion { get; set; }  // مرافق شركة الكهرباء الميداني
    
    // Notes
    public string? Notes { get; set; }  // ملاحظات عمومية
    
    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid CreatedById { get; set; }
    public User? CreatedBy { get; set; }

}
