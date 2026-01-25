using FormApp.Core.Enums;

namespace FormApp.Application.DTOs.Transactions;

public class CreateTransactionRecordDto
{
    public string FacilityName { get; set; } = string.Empty;
    public string SubscriptionNumber { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string Mobile1 { get; set; } = string.Empty;
    public string? Mobile2 { get; set; }
    
    public string SubscriberName { get; set; } = string.Empty;
    public SubscriptionType SubscriptionType { get; set; }
    
    public SubscriptionMenuType ListBuildingType { get; set; }
    public SubscriptionScanType ActualBuildingType { get; set; }
    public BeneficiaryStatus BeneficiaryStatus { get; set; }
    
    public string MeasurementNumber { get; set; } = string.Empty;
    public string ActualMeasurementNumber { get; set; } = string.Empty;
    public MeasurementUnitActual? MeasurementUnitActual { get; set; }
    public MeasurementUnitScan? MeasurementUnitScan { get; set; }
    public string ReadingNumber { get; set; } = string.Empty;
    public bool IsNotRealReadingNumber { get; set; }
    public string MultiplicationFactor { get; set; } = string.Empty;
    public MeterStatus MeterStatus { get; set; }    public string? MeterNotes { get; set; }    
    public AddressStatus AddressStatus { get; set; }
    public AnyAddress AnyAddress { get; set; }
    
    public bool NoMatchingList { get; set; }
    public bool NoMatchingMeasurement { get; set; }
    public bool HasDestructionOrDamage { get; set; }
    public bool IsSharedMeter { get; set; }
    public bool HasUnitsFromOtherSubscriptions { get; set; }
    public bool HasUnitsBeforeMeter { get; set; }
    public bool Other { get; set; }
    
    public int? BenefitingUnitsCount { get; set; }
    public int? OtherUnitsAfterMeterCount { get; set; }
    public int? OtherUnitsBeforeMeterCount { get; set; }
    
    // Transformer Information
    public string? TransformerCapacity { get; set; }
    public string? TransformerSerialNumber { get; set; }
    public string? ManufacturingCompany { get; set; }
    public bool IsTransformerWorking { get; set; }
    
    // Geographic Information
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? NearestPoint { get; set; }
    public string? EstimatedNeighborhood { get; set; }
    public string? ActualNeighborhood { get; set; }
    public int? EstimatedAlley { get; set; }
    public int? EstimatedHouseNumber { get; set; }
    public int? ActualAlley { get; set; }
    public int? ActualHouseNumber { get; set; }
    public string? EstimatedDetails { get; set; }
    public string? ActualDetails { get; set; }
    
    // Branch Information (for Transformer)
    public bool HasBranches { get; set; }
    public List<BranchDto>? Branches { get; set; }
    
    // Room Information
    public bool HasRoomButtons { get; set; }
    public bool IsRoomSuitable { get; set; }
    public bool CanAddPartitions { get; set; }
    
    // Additional Information
    public string? CommercialAccountName { get; set; }
    public string? FieldPersonName { get; set; }
    public string? FieldElectricCompanyCompanion { get; set; }
    public string? SubscriberNotes { get; set; }
    public string? TransformerNotes { get; set; }
    
    public string? Notes { get; set; }
}

public class TransactionRecordResponseDto
{
    public Guid Id { get; set; }
    public string FacilityName { get; set; } = string.Empty;
    public string SubscriptionNumber { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string Mobile1 { get; set; } = string.Empty;
    public string? Mobile2 { get; set; }
    public string SubscriberName { get; set; } = string.Empty;
    public SubscriptionType SubscriptionType { get; set; }
    public SubscriptionMenuType ListBuildingType { get; set; }
    public SubscriptionScanType ActualBuildingType { get; set; }
    public BeneficiaryStatus BeneficiaryStatus { get; set; }
    public string MeasurementNumber { get; set; } = string.Empty;
    public string ActualMeasurementNumber { get; set; } = string.Empty;
    public MeasurementUnitActual? MeasurementUnitActual { get; set; }
    public MeasurementUnitScan? MeasurementUnitScan { get; set; }
    public string ReadingNumber { get; set; } = string.Empty;
    public bool IsNotRealReadingNumber { get; set; }
    public string MultiplicationFactor { get; set; } = string.Empty;
    public MeterStatus MeterStatus { get; set; }
    public string? MeterNotes { get; set; }
    public AddressStatus AddressStatus { get; set; }
    public AnyAddress AnyAddress { get; set; }
    public bool NoMatchingList { get; set; }
    public bool NoMatchingMeasurement { get; set; }
    public bool HasDestructionOrDamage { get; set; }
    public bool IsSharedMeter { get; set; }
    public bool HasUnitsFromOtherSubscriptions { get; set; }
    public bool HasUnitsBeforeMeter { get; set; }
    public bool Other { get; set; }
    public int? BenefitingUnitsCount { get; set; }
    public int? OtherUnitsAfterMeterCount { get; set; }
    public int? OtherUnitsBeforeMeterCount { get; set; }
    
    // Transformer Information
    public string? TransformerCapacity { get; set; }
    public string? TransformerSerialNumber { get; set; }
    public string? ManufacturingCompany { get; set; }
    public bool IsTransformerWorking { get; set; }
    
    // Geographic Information
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? NearestPoint { get; set; }
    public string? EstimatedNeighborhood { get; set; }
    public string? ActualNeighborhood { get; set; }
    public int? EstimatedAlley { get; set; }
    public int? EstimatedHouseNumber { get; set; }
    public int? ActualAlley { get; set; }
    public int? ActualHouseNumber { get; set; }
    public string? EstimatedDetails { get; set; }
    public string? ActualDetails { get; set; }
    
    // Branch Information (for Transformer)
    public bool HasBranches { get; set; }
    public List<BranchResponseDto>? Branches { get; set; }
    
    // Room Information
    public bool HasRoomButtons { get; set; }
    public bool IsRoomSuitable { get; set; }
    public bool CanAddPartitions { get; set; }
    
    // Additional Information
    public string? CommercialAccountName { get; set; }
    public string? FieldPersonName { get; set; }
    public string? FieldElectricCompanyCompanion { get; set; }
    public string? SubscriberNotes { get; set; }
    public string? TransformerNotes { get; set; }
    
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public List<TransactionAttachmentResponseDto> Attachments { get; set; } = new();
}

public class BranchDto
{
    public BranchType? BranchType { get; set; }
    public int BranchCount { get; set; }
    public string? Name { get; set; }
    public string? Size { get; set; }
    public string? Notes { get; set; }
}

public class BranchResponseDto
{
    public Guid Id { get; set; }
    public BranchType? BranchType { get; set; }
    public int BranchCount { get; set; }
    public string? Name { get; set; }
    public string? Size { get; set; }
    public string? Notes { get; set; }
}
