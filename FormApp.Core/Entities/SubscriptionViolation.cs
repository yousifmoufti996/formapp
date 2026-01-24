namespace FormApp.Core.Entities;

public class SubscriptionViolation
{
    public Guid Id { get; set; }
    
    // Violation Flags
    public bool NoMatchingList { get; set; }  // لا يوجد تجاوز فني
    public bool NoMatchingMeasurement { get; set; }  // اشتراك له قائمة وليس له مقياس
    public bool HasDestructionOrDamage { get; set; }  // يوجد مقياس تم تخريبه أو فقده
    public bool IsSharedMeter { get; set; }  // مشترك لديه وصلة خارج المقياس
    public bool HasUnitsFromOtherSubscriptions { get; set; }  // مشترك مع وصلات بعد المقياس لوحدات أخرى
    public bool HasUnitsBeforeMeter { get; set; }  // مشترك لديه وصلات قبل المقياس لوحدات أخرى
    public bool Other { get; set; }  //  أخرى
    
    // Units Count
    public int? BenefitingUnitsCount { get; set; }  // عدد الوحدات المستفيدة من الاشتراك
    public int? OtherUnitsAfterMeterCount { get; set; }  // بعد عدد الوحدات المستفيدة من الاشتراك
    public int? OtherUnitsBeforeMeterCount { get; set; }  // قبل عدد الوحدات المستفيدة من الاشتراك
    
    // Navigation property
    public Transaction? Transaction { get; set; }
}
