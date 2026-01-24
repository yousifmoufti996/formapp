using System.ComponentModel;

namespace FormApp.Core.Enums;

public enum SubscriptionType
{
    [Description("اشتراك له قائمة (نظامي)")]
    Regular = 1,
    [Description("اشتراك ليس له قائمة (غير نظامي)")]
    Irregular = 2,
    [Description("اشتراك غير متعاون")]
    Uncooperative = 3
}

public enum BeneficiaryStatus
{
    [Description("مالك")]
    Owner = 1,
    [Description("مستأجر")]
    Tenant = 2,
    [Description("غير محدد")]
    Unspecified = 3
}

public enum SubscriptionMenuType
{
    [Description("منزلي")]
    Residential = 1,
    [Description("تجاري")]
    Commercial = 2,
    [Description("صناعي")]
    Industrial = 3,
    [Description("زراعي")]
    Agricultural = 4,
    [Description("حكومي")]
    Government = 5,
    [Description("بطاقة حمراء")]
    RedCrescent = 6
}

public enum SubscriptionScanType
{
    [Description("منزلي")]
    Residential = 1,
    [Description("تجاري")]
    Commercial = 2,
    [Description("صناعي")]
    Industrial = 3,
    [Description("زراعي")]
    Agricultural = 4,
    [Description("حكومي")]
    Government = 5,
    [Description("بطاقة حمراء")]
    RedCrescent = 6
}

public enum MeterStatus
{
    [Description("نشط")]
    Active = 1,
    [Description("مغلق")]
    Closed = 2,
    [Description("مفقود")]
    Lost = 3
}

public enum AddressStatus
{
    [Description("يوجد عنوان")]
    HasAddress = 1,
    [Description("لا يوجد عنوان")]
    NoAddress = 2
}

public enum AnyAddress
{
    [Description("مغلق")]
    Closed = 1,
    [Description("مهجور")]
    Abandoned = 2
}

public enum MeasurementUnitActual
{
    [Description("احادي الطور")]
    SinglePhase = 1,
    [Description("ثلاثي الطور")]
    ThreePhase = 2
}
public enum MeasurementUnitScan
{
    [Description("احادي الطور")]
    SinglePhase = 1,
    [Description("ثلاثي الطور")]
    ThreePhase = 2
}

public enum BranchType
{
    [Description("نوع 1")]
    Type1 = 1,
    [Description("نوع 2")]
    Type2 = 2,
    [Description("نوع 3")]
    Type3 = 3,
    [Description("نوع 4")]
    Type4 = 4
}

public enum TransformerCapacity
{
    [Description("25 KVA")]
    KVA_25 = 0,
    [Description("50 KVA")]
    KVA_50 = 1,
    [Description("63 KVA")]
    KVA_63 = 2,
    [Description("100 KVA")]
    KVA_100 = 3,
    [Description("160 KVA")]
    KVA_160 = 4,
    [Description("200 KVA")]
    KVA_200 = 5,
    [Description("250 KVA")]
    KVA_250 = 6,
    [Description("315 KVA")]
    KVA_315 = 7,
    [Description("400 KVA")]
    KVA_400 = 8,
    [Description("500 KVA")]
    KVA_500 = 9,
    [Description("630 KVA")]
    KVA_630 = 10,
    [Description("800 KVA")]
    KVA_800 = 11,
    [Description("1000 KVA")]
    KVA_1000 = 12,
    [Description("1250 KVA")]
    KVA_1250 = 13,
    [Description("1600 KVA")]
    KVA_1600 = 14,
    [Description("2000 KVA")]
    KVA_2000 = 15,
    [Description("2500 KVA")]
    KVA_2500 = 16,
    [Description("3150 KVA")]
    KVA_3150 = 17,
    [Description("5000 KVA")]
    KVA_5000 = 18,
    [Description("10000 KVA")]
    KVA_10000 = 19
}
