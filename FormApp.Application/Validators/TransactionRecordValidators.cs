using FluentValidation;
using FormApp.Application.DTOs.Transactions;

namespace FormApp.Application.Validators;

public class CreateTransactionRecordValidator : AbstractValidator<CreateTransactionRecordDto>
{
    public CreateTransactionRecordValidator()
    {
        RuleFor(x => x.FacilityName)
            .NotEmpty().WithMessage("Facility name is required")
            .MaximumLength(200).WithMessage("Facility name cannot exceed 200 characters");

        RuleFor(x => x.SubscriptionNumber)
            .MaximumLength(50).WithMessage("Subscription number cannot exceed 50 characters");

        RuleFor(x => x.AccountNumber)
            .MaximumLength(50).WithMessage("Account number cannot exceed 50 characters");

        RuleFor(x => x.Mobile1)
            .MaximumLength(20).WithMessage("Mobile 1 cannot exceed 20 characters");

        RuleFor(x => x.Mobile2)
            .MaximumLength(20).WithMessage("Mobile 2 cannot exceed 20 characters");

        RuleFor(x => x.SubscriberName)
            .MaximumLength(200).WithMessage("Subscriber name cannot exceed 200 characters");

        RuleFor(x => x.MeasurementNumber)
            .MaximumLength(50).WithMessage("Measurement number cannot exceed 50 characters");

        RuleFor(x => x.ActualMeasurementNumber)
            .MaximumLength(50).WithMessage("Actual measurement number cannot exceed 50 characters");

        RuleFor(x => x.ReadingNumber)
            .MaximumLength(50).WithMessage("Reading number cannot exceed 50 characters");

        RuleFor(x => x.MultiplicationFactor)
            .NotEmpty().WithMessage("Multiplication factor is required")
            .MaximumLength(20).WithMessage("Multiplication factor cannot exceed 20 characters");

        // Geographic location validation - if one is provided, both must be provided
        RuleFor(x => x.Latitude)
            .NotNull().WithMessage("Latitude is required when Longitude is provided")
            .When(x => x.Longitude.HasValue);

        RuleFor(x => x.Longitude)
            .NotNull().WithMessage("Longitude is required when Latitude is provided")
            .When(x => x.Latitude.HasValue);

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90")
            .When(x => x.Latitude.HasValue);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180")
            .When(x => x.Longitude.HasValue);

        RuleFor(x => x.TransformerCapacity)
            .MaximumLength(50).WithMessage("Transformer capacity cannot exceed 50 characters");

        RuleFor(x => x.TransformerSerialNumber)
            .MaximumLength(100).WithMessage("Transformer serial number cannot exceed 100 characters");

        RuleFor(x => x.NearestPoint)
            .MaximumLength(200).WithMessage("Nearest point cannot exceed 200 characters");

        RuleFor(x => x.CommercialAccountName)
            .MaximumLength(200).WithMessage("Commercial account name cannot exceed 200 characters");

        RuleFor(x => x.FieldPersonName)
            .MaximumLength(100).WithMessage("Field person name cannot exceed 100 characters");

        RuleFor(x => x.FieldElectricCompanyCompanion)
            .MaximumLength(100).WithMessage("Field electric company companion cannot exceed 100 characters");

        RuleFor(x => x.BenefitingUnitsCount)
            .GreaterThanOrEqualTo(0).WithMessage("Benefiting units count must be greater than or equal to 0");

        RuleFor(x => x.OtherUnitsAfterMeterCount)
            .GreaterThanOrEqualTo(0).WithMessage("Other units after meter count must be greater than or equal to 0");

        RuleFor(x => x.OtherUnitsBeforeMeterCount)
            .GreaterThanOrEqualTo(0).WithMessage("Other units before meter count must be greater than or equal to 0");

        RuleFor(x => x.ManufacturingCompany)
            .MaximumLength(200).WithMessage("Manufacturing company cannot exceed 200 characters");
    }
}
