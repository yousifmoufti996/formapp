using FormApp.Application.DTOs.Transactions;
using FormApp.Application.Interfaces;
using FormApp.Core.Entities;
using FormApp.Core.Exceptions;
using FormApp.Core.IRepositories;

namespace FormApp.Application.Services;

public class TransactionRecordService : ITransactionRecordService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ISubscriberRepository _subscriberRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IMeterScaleRepository _meterScaleRepository;
    private readonly ISubscriptionViolationRepository _violationRepository;
    private readonly ITransformerRepository _transformerRepository;
    private readonly IFileService _fileService;

    public TransactionRecordService(
        ITransactionRepository transactionRepository,
        ISubscriberRepository subscriberRepository,
        ISubscriptionRepository subscriptionRepository,
        IMeterScaleRepository meterScaleRepository,
        ISubscriptionViolationRepository violationRepository,
        ITransformerRepository transformerRepository,
        IFileService fileService)
    {
        _transactionRepository = transactionRepository;
        _subscriberRepository = subscriberRepository;
        _subscriptionRepository = subscriptionRepository;
        _meterScaleRepository = meterScaleRepository;
        _violationRepository = violationRepository;
        _transformerRepository = transformerRepository;
        _fileService = fileService;
    }

    public async Task<IEnumerable<TransactionRecordResponseDto>> GetAllAsync(Guid userId)
    {
        var transactions = await _transactionRepository.GetAllAsync(userId);
        var responseDtos = new List<TransactionRecordResponseDto>();
        
        foreach (var transaction in transactions)
        {
            responseDtos.Add(MapTransactionToResponseDto(transaction, includeSingleAttachment: true));
        }
        
        return responseDtos;
    }

    public async Task<TransactionRecordResponseDto> GetByIdAsync(Guid id, Guid userId)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id, userId);
        if (transaction == null)
        {
            throw new NotFoundException($"Transaction with ID {id} not found or you don't have permission to access it.");
        }

        return MapTransactionToResponseDto(transaction, includeSingleAttachment: false);
    }

    public async Task<TransactionRecordResponseDto> CreateAsync(CreateTransactionRecordDto dto, Guid currentUserId)
    {
        // Check if Subscriber already exists by AccountNumber or SubscriptionNumber
        Subscriber? existingSubscriber = null;
        
        if (!string.IsNullOrWhiteSpace(dto.AccountNumber))
        {
            existingSubscriber = await _subscriberRepository.GetByAccountNumberAsync(dto.AccountNumber);
        }
        
        if (existingSubscriber == null && !string.IsNullOrWhiteSpace(dto.SubscriptionNumber))
        {
            existingSubscriber = await _subscriberRepository.GetBySubscriptionNumberAsync(dto.SubscriptionNumber);
        }
        
        Subscriber createdSubscriber;
        
        if (existingSubscriber != null)
        {
            // Check if subscriber already has a transaction
            var existingTransaction = await _transactionRepository.GetBySubscriberIdAsync(existingSubscriber.Id);
            if (existingTransaction != null)
            {
                throw new BadRequestException($"A transaction already exists for this subscriber (Account: {existingSubscriber.AccountNumber}, Subscription: {existingSubscriber.SubscriptionNumber}). Transaction ID: {existingTransaction.Id}");
            }
            
            // Update existing subscriber
            existingSubscriber.FacilityName = dto.FacilityName;
            existingSubscriber.SubscriptionNumber = dto.SubscriptionNumber;
            existingSubscriber.AccountNumber = dto.AccountNumber;
            existingSubscriber.Mobile1 = dto.Mobile1;
            existingSubscriber.Mobile2 = dto.Mobile2;
            existingSubscriber.AddressStatus = dto.AddressStatus;
            existingSubscriber.AnyAddress = dto.AnyAddress;
            existingSubscriber.EstimatedNeighborhood = dto.EstimatedNeighborhood;
            existingSubscriber.ActualNeighborhood = dto.ActualNeighborhood;
            existingSubscriber.Latitude = dto.Latitude;
            existingSubscriber.Longitude = dto.Longitude;
            existingSubscriber.NearestPoint = dto.NearestPoint;
            existingSubscriber.EstimatedAlley = dto.EstimatedAlley;
            existingSubscriber.EstimatedHouseNumber = dto.EstimatedHouseNumber;
            existingSubscriber.ActualAlley = dto.ActualAlley;
            existingSubscriber.ActualHouseNumber = dto.ActualHouseNumber;
            existingSubscriber.EstimatedDetails = dto.EstimatedDetails;
            existingSubscriber.ActualDetails = dto.ActualDetails;
            existingSubscriber.CommercialAccountName = dto.CommercialAccountName;
            existingSubscriber.FieldPersonName = dto.FieldPersonName;
            existingSubscriber.FieldElectricCompanyCompanion = dto.FieldElectricCompanyCompanion;
            
            createdSubscriber = await _subscriberRepository.UpdateAsync(existingSubscriber);
        }
        else
        {
            // Create new Subscriber
            var subscriber = new Subscriber
            {
                FacilityName = dto.FacilityName,
                SubscriptionNumber = dto.SubscriptionNumber,
                AccountNumber = dto.AccountNumber,
                Mobile1 = dto.Mobile1,
                Mobile2 = dto.Mobile2,
                AddressStatus = dto.AddressStatus,
                AnyAddress = dto.AnyAddress,
                EstimatedNeighborhood = dto.EstimatedNeighborhood,
                ActualNeighborhood = dto.ActualNeighborhood,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                NearestPoint = dto.NearestPoint,
                EstimatedAlley = dto.EstimatedAlley,
                EstimatedHouseNumber = dto.EstimatedHouseNumber,
                ActualAlley = dto.ActualAlley,
                ActualHouseNumber = dto.ActualHouseNumber,
                EstimatedDetails = dto.EstimatedDetails,
                ActualDetails = dto.ActualDetails,
                CommercialAccountName = dto.CommercialAccountName,
                FieldPersonName = dto.FieldPersonName,
                FieldElectricCompanyCompanion = dto.FieldElectricCompanyCompanion
            };
            createdSubscriber = await _subscriberRepository.AddAsync(subscriber);
        }

        // Create Subscription
        var subscription = new Subscription
        {
            SubscriberName = dto.SubscriberName,
            SubscriptionType = dto.SubscriptionType,
            ListBuildingType = dto.ListBuildingType,
            ActualBuildingType = dto.ActualBuildingType,
            BeneficiaryStatus = dto.BeneficiaryStatus
        };
        var createdSubscription = await _subscriptionRepository.AddAsync(subscription);

        // Create MeterScale
        var meterScale = new MeterScale
        {
            MeasurementNumber = dto.MeasurementNumber,
            ActualMeasurementNumber = dto.ActualMeasurementNumber,
            MeasurementUnitActual = dto.MeasurementUnitActual,
            MeasurementUnitScan = dto.MeasurementUnitScan,
            ReadingNumber = dto.ReadingNumber,
            IsNotRealReadingNumber = dto.IsNotRealReadingNumber,
            MultiplicationFactor = dto.MultiplicationFactor,
            MeterStatus = dto.MeterStatus
        };
        var createdMeterScale = await _meterScaleRepository.AddAsync(meterScale);

        // Create SubscriptionViolation
        var violation = new SubscriptionViolation
        {
            NoMatchingList = dto.NoMatchingList,
            NoMatchingMeasurement = dto.NoMatchingMeasurement,
            HasDestructionOrDamage = dto.HasDestructionOrDamage,
            IsSharedMeter = dto.IsSharedMeter,
            HasUnitsFromOtherSubscriptions = dto.HasUnitsFromOtherSubscriptions,
            HasUnitsBeforeMeter = dto.HasUnitsBeforeMeter,
            Other = dto.Other,
            BenefitingUnitsCount = dto.BenefitingUnitsCount,
            OtherUnitsAfterMeterCount = dto.OtherUnitsAfterMeterCount,
            OtherUnitsBeforeMeterCount = dto.OtherUnitsBeforeMeterCount
        };
        var createdViolation = await _violationRepository.AddAsync(violation);

        // Create Transformer
        var transformer = new Transformer
        {
            TransformerCapacity = string.IsNullOrEmpty(dto.TransformerCapacity) ? null : Enum.TryParse<Core.Enums.TransformerCapacity>(dto.TransformerCapacity, out var capacity) ? capacity : null,
            TransformerSerialNumber = dto.TransformerSerialNumber,
            ManufacturingCompany = dto.ManufacturingCompany,
            IsTransformerWorking = dto.IsTransformerWorking,
            HasBranches = dto.HasBranches,
            HasRoomButtons = dto.HasRoomButtons,
            IsRoomSuitable = dto.IsRoomSuitable,
            CanAddPartitions = dto.CanAddPartitions
        };
        var createdTransformer = await _transformerRepository.AddAsync(transformer);

        // Create Branches if needed
        if (dto.HasBranches && dto.Branches != null && dto.Branches.Any())
        {
            foreach (var branchDto in dto.Branches)
            {
                var branch = new SubscriptionBranch
                {
                    BranchType = branchDto.BranchType,
                    BranchCount = branchDto.BranchCount,
                    Name = branchDto.Name,
                    Size = branchDto.Size,
                    Notes = branchDto.Notes,
                    TransformerId = createdTransformer.Id
                };
                await _transformerRepository.UpdateAsync(createdTransformer); // Ensure branches are added
            }
        }

        // Create Transaction
        var transaction = new Transaction
        {
            SubscriberId = createdSubscriber.Id,
            SubscriptionId = createdSubscription.Id,
            MeterScaleId = createdMeterScale.Id,
            ViolationId = createdViolation.Id,
            TransformerId = createdTransformer.Id,
            Notes = dto.Notes,
            CreatedById = currentUserId
        };

        var createdTransaction = await _transactionRepository.AddAsync(transaction);
        
        // Fetch the complete transaction with all includes
        var fullTransaction = await _transactionRepository.GetByIdAsync(createdTransaction.Id);
        return MapTransactionToResponseDto(fullTransaction!, includeSingleAttachment: false);
    }

    public async Task<TransactionRecordResponseDto> UpdateAsync(Guid id, CreateTransactionRecordDto dto)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
        {
            throw new NotFoundException($"Transaction with ID {id} not found.");
        }

        // Update Subscriber
        if (transaction.Subscriber != null)
        {
            transaction.Subscriber.FacilityName = dto.FacilityName;
            transaction.Subscriber.SubscriptionNumber = dto.SubscriptionNumber;
            transaction.Subscriber.AccountNumber = dto.AccountNumber;
            transaction.Subscriber.Mobile1 = dto.Mobile1;
            transaction.Subscriber.Mobile2 = dto.Mobile2;
            transaction.Subscriber.AddressStatus = dto.AddressStatus;
            transaction.Subscriber.AnyAddress = dto.AnyAddress;
            transaction.Subscriber.EstimatedNeighborhood = dto.EstimatedNeighborhood;
            transaction.Subscriber.ActualNeighborhood = dto.ActualNeighborhood;
            transaction.Subscriber.Latitude = dto.Latitude;
            transaction.Subscriber.Longitude = dto.Longitude;
            transaction.Subscriber.NearestPoint = dto.NearestPoint;
            transaction.Subscriber.EstimatedAlley = dto.EstimatedAlley;
            transaction.Subscriber.EstimatedHouseNumber = dto.EstimatedHouseNumber;
            transaction.Subscriber.ActualAlley = dto.ActualAlley;
            transaction.Subscriber.ActualHouseNumber = dto.ActualHouseNumber;
            transaction.Subscriber.EstimatedDetails = dto.EstimatedDetails;
            transaction.Subscriber.ActualDetails = dto.ActualDetails;
            transaction.Subscriber.CommercialAccountName = dto.CommercialAccountName;
            transaction.Subscriber.FieldPersonName = dto.FieldPersonName;
            transaction.Subscriber.FieldElectricCompanyCompanion = dto.FieldElectricCompanyCompanion;
            await _subscriberRepository.UpdateAsync(transaction.Subscriber);
        }

        // Update Subscription
        if (transaction.Subscription != null)
        {
            transaction.Subscription.SubscriberName = dto.SubscriberName;
            transaction.Subscription.SubscriptionType = dto.SubscriptionType;
            transaction.Subscription.ListBuildingType = dto.ListBuildingType;
            transaction.Subscription.ActualBuildingType = dto.ActualBuildingType;
            transaction.Subscription.BeneficiaryStatus = dto.BeneficiaryStatus;
            await _subscriptionRepository.UpdateAsync(transaction.Subscription);
        }

        // Update MeterScale
        if (transaction.MeterScale != null)
        {
            transaction.MeterScale.MeasurementNumber = dto.MeasurementNumber;
            transaction.MeterScale.ActualMeasurementNumber = dto.ActualMeasurementNumber;
            transaction.MeterScale.MeasurementUnitActual = dto.MeasurementUnitActual;
            transaction.MeterScale.MeasurementUnitScan = dto.MeasurementUnitScan;
            transaction.MeterScale.ReadingNumber = dto.ReadingNumber;
            transaction.MeterScale.IsNotRealReadingNumber = dto.IsNotRealReadingNumber;
            transaction.MeterScale.MultiplicationFactor = dto.MultiplicationFactor;
            transaction.MeterScale.MeterStatus = dto.MeterStatus;
            await _meterScaleRepository.UpdateAsync(transaction.MeterScale);
        }

        // Update Violation
        if (transaction.Violation != null)
        {
            transaction.Violation.NoMatchingList = dto.NoMatchingList;
            transaction.Violation.NoMatchingMeasurement = dto.NoMatchingMeasurement;
            transaction.Violation.HasDestructionOrDamage = dto.HasDestructionOrDamage;
            transaction.Violation.IsSharedMeter = dto.IsSharedMeter;
            transaction.Violation.HasUnitsFromOtherSubscriptions = dto.HasUnitsFromOtherSubscriptions;
            transaction.Violation.HasUnitsBeforeMeter = dto.HasUnitsBeforeMeter;
            transaction.Violation.Other = dto.Other;
            transaction.Violation.BenefitingUnitsCount = dto.BenefitingUnitsCount;
            transaction.Violation.OtherUnitsAfterMeterCount = dto.OtherUnitsAfterMeterCount;
            transaction.Violation.OtherUnitsBeforeMeterCount = dto.OtherUnitsBeforeMeterCount;
            await _violationRepository.UpdateAsync(transaction.Violation);
        }

        // Update Transformer
        if (transaction.Transformer != null)
        {
            transaction.Transformer.TransformerCapacity = string.IsNullOrEmpty(dto.TransformerCapacity) ? null : Enum.TryParse<Core.Enums.TransformerCapacity>(dto.TransformerCapacity, out var capacity) ? capacity : null;
            transaction.Transformer.TransformerSerialNumber = dto.TransformerSerialNumber;
            transaction.Transformer.ManufacturingCompany = dto.ManufacturingCompany;
            transaction.Transformer.IsTransformerWorking = dto.IsTransformerWorking;
            transaction.Transformer.HasBranches = dto.HasBranches;
            transaction.Transformer.HasRoomButtons = dto.HasRoomButtons;
            transaction.Transformer.IsRoomSuitable = dto.IsRoomSuitable;
            transaction.Transformer.CanAddPartitions = dto.CanAddPartitions;
            
            // Update Branches
            transaction.Transformer.Branches.Clear();
            if (dto.HasBranches && dto.Branches != null && dto.Branches.Any())
            {
                foreach (var branchDto in dto.Branches)
                {
                    transaction.Transformer.Branches.Add(new SubscriptionBranch
                    {
                        BranchType = branchDto.BranchType,
                        BranchCount = branchDto.BranchCount,
                        Name = branchDto.Name,
                        Size = branchDto.Size,
                        Notes = branchDto.Notes,
                        TransformerId = transaction.Transformer.Id
                    });
                }
            }
            
            await _transformerRepository.UpdateAsync(transaction.Transformer);
        }

        // Update Transaction itself
        transaction.Notes = dto.Notes;
        await _transactionRepository.UpdateAsync(transaction);

        // Fetch the updated transaction with all includes
        var updatedTransaction = await _transactionRepository.GetByIdAsync(transaction.Id);
        return MapTransactionToResponseDto(updatedTransaction!, includeSingleAttachment: false);
    }

    public async Task DeleteAsync(Guid id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
        {
            throw new NotFoundException($"Transaction with ID {id} not found.");
        }

        await _transactionRepository.DeleteAsync(id);
    }

    private TransactionRecordResponseDto MapTransactionToResponseDto(Transaction transaction, bool includeSingleAttachment = false)
    {
        var subscriber = transaction.Subscriber;
        var subscription = transaction.Subscription;
        var meterScale = transaction.MeterScale;
        var violation = transaction.Violation;
        var transformer = transaction.Transformer;

        // Determine single image URL for GetAll
        string? imageUrl = null;
        List<TransactionAttachment>? attachmentsToInclude = null;
        
        if (includeSingleAttachment && transaction.Attachments != null && transaction.Attachments.Any())
        {
            // First, try to find PropertyPhoto (AttachmentType = 4)
            var selectedAttachment = transaction.Attachments.FirstOrDefault(a => a.FileType == Core.Enums.AttachmentType.PropertyPhoto);
            
            if (selectedAttachment == null)
            {
                // If no PropertyPhoto, get the oldest one by CreatedAt
                selectedAttachment = transaction.Attachments.OrderBy(a => a.CreatedAt).FirstOrDefault();
            }
            
            if (selectedAttachment?.File != null)
            {
                imageUrl = _fileService.GetFileUrl(selectedAttachment.File.FilePath);
            }
        }
        else if (!includeSingleAttachment)
        {
            // Include all attachments for GetById
            attachmentsToInclude = transaction.Attachments?.ToList();
        }

        return new TransactionRecordResponseDto
        {
            Id = transaction.Id,
            FacilityName = subscriber?.FacilityName ?? string.Empty,
            SubscriptionNumber = subscriber?.SubscriptionNumber ?? string.Empty,
            AccountNumber = subscriber?.AccountNumber ?? string.Empty,
            Mobile1 = subscriber?.Mobile1 ?? string.Empty,
            Mobile2 = subscriber?.Mobile2,
            SubscriberName = subscription?.SubscriberName ?? string.Empty,
            SubscriptionType = subscription?.SubscriptionType ?? default,
            ListBuildingType = subscription?.ListBuildingType ?? default,
            ActualBuildingType = subscription?.ActualBuildingType ?? default,
            BeneficiaryStatus = subscription?.BeneficiaryStatus ?? default,
            MeasurementNumber = meterScale?.MeasurementNumber ?? string.Empty,
            ActualMeasurementNumber = meterScale?.ActualMeasurementNumber ?? string.Empty,
            MeasurementUnitActual = meterScale?.MeasurementUnitActual,
            MeasurementUnitScan = meterScale?.MeasurementUnitScan,
            ReadingNumber = meterScale?.ReadingNumber ?? string.Empty,
            IsNotRealReadingNumber = meterScale?.IsNotRealReadingNumber ?? false,
            MultiplicationFactor = meterScale?.MultiplicationFactor ?? string.Empty,
            MeterStatus = meterScale?.MeterStatus ?? default,
            AddressStatus = subscriber?.AddressStatus ?? default,
            AnyAddress = subscriber?.AnyAddress ?? default,
            NoMatchingList = violation?.NoMatchingList ?? false,
            NoMatchingMeasurement = violation?.NoMatchingMeasurement ?? false,
            HasDestructionOrDamage = violation?.HasDestructionOrDamage ?? false,
            IsSharedMeter = violation?.IsSharedMeter ?? false,
            HasUnitsFromOtherSubscriptions = violation?.HasUnitsFromOtherSubscriptions ?? false,
            HasUnitsBeforeMeter = violation?.HasUnitsBeforeMeter ?? false,
            Other = violation?.Other ?? false,
            BenefitingUnitsCount = violation?.BenefitingUnitsCount ?? 0,
            OtherUnitsAfterMeterCount = violation?.OtherUnitsAfterMeterCount ?? 0,
            OtherUnitsBeforeMeterCount = violation?.OtherUnitsBeforeMeterCount ?? 0,
            TransformerCapacity = transformer?.TransformerCapacity?.ToString(),
            TransformerSerialNumber = transformer?.TransformerSerialNumber,
            ManufacturingCompany = transformer?.ManufacturingCompany,
            IsTransformerWorking = transformer?.IsTransformerWorking ?? false,
            Latitude = subscriber?.Latitude,
            Longitude = subscriber?.Longitude,
            NearestPoint = subscriber?.NearestPoint,
            EstimatedNeighborhood = subscriber?.EstimatedNeighborhood,
            ActualNeighborhood = subscriber?.ActualNeighborhood,
            EstimatedAlley = subscriber?.EstimatedAlley,
            EstimatedHouseNumber = subscriber?.EstimatedHouseNumber,
            ActualAlley = subscriber?.ActualAlley,
            ActualHouseNumber = subscriber?.ActualHouseNumber,
            EstimatedDetails = subscriber?.EstimatedDetails,
            ActualDetails = subscriber?.ActualDetails,
            HasBranches = transformer?.HasBranches ?? false,
            Branches = transformer?.Branches?.Select(b => new BranchResponseDto
            {
                Id = b.Id,
                BranchType = b.BranchType,
                BranchCount = b.BranchCount,
                Name = b.Name,
                Size = b.Size,
                Notes = b.Notes
            }).ToList(),
            HasRoomButtons = transformer?.HasRoomButtons ?? false,
            IsRoomSuitable = transformer?.IsRoomSuitable ?? false,
            CanAddPartitions = transformer?.CanAddPartitions ?? false,
            CommercialAccountName = subscriber?.CommercialAccountName,
            FieldPersonName = subscriber?.FieldPersonName,
            FieldElectricCompanyCompanion = subscriber?.FieldElectricCompanyCompanion,
            Notes = transaction.Notes,
            CreatedAt = transaction.CreatedAt,
            UpdatedAt = transaction.UpdatedAt,
            CreatedByName = transaction.CreatedBy != null ? $"{transaction.CreatedBy.FirstName} {transaction.CreatedBy.LastName}".Trim() : string.Empty,
            ImageUrl = imageUrl,
            Attachments = attachmentsToInclude?.Select(a => new TransactionAttachmentResponseDto
            {
                Id = a.Id,
                TransactionId = a.TransactionId,
                Name = a.Name,
                FileType = a.FileType,
                FileId = a.FileId,
                FileName = a.File?.FileName ?? string.Empty,
                FileExtension = a.File?.FileExtension ?? string.Empty,
                FileUrl = a.File != null ? _fileService.GetFileUrl(a.File.FilePath) : string.Empty,
                FileSize = a.File?.FileSize ?? 0,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                CreatedByName = a.CreatedBy != null ? $"{a.CreatedBy.FirstName} {a.CreatedBy.LastName}".Trim() : string.Empty
            }).ToList() ?? new List<TransactionAttachmentResponseDto>()
        };
    }
}
