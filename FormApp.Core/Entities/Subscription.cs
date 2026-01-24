using FormApp.Core.Enums;

namespace FormApp.Core.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    
    // Subscriber Information
    public string SubscriberName { get; set; } = string.Empty;  // اسم المشترك
    public SubscriptionType SubscriptionType { get; set; }  // نوع الاشتراك
    
    // Facility Classification
    public SubscriptionMenuType ListBuildingType { get; set; }  // صنف الاشتراك حسب القائمة
    public SubscriptionScanType ActualBuildingType { get; set; }  // صنف الاشتراك حسب المسح
    public BeneficiaryStatus BeneficiaryStatus { get; set; }  // حالة المنتفع من الاشتراك
    
    // Navigation property
    public Transaction? Transaction { get; set; }
}
