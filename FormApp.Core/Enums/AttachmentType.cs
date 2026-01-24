using System.ComponentModel;

namespace FormApp.Core.Enums;

public enum AttachmentType
{
    [Description("صورة وصل الاشتراك")]
    SubscriptionReceipt = 1,
    
    [Description("صورة المقياس")]
    MeterPhoto = 2,
    
    [Description("صورة بيانات المحولة")]
    TransformerData = 3,
    
    [Description("صورة العقار")]
    PropertyPhoto = 4
}
