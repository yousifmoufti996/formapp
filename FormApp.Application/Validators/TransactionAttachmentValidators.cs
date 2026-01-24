using FluentValidation;
using FormApp.Application.DTOs.Transactions;

namespace FormApp.Application.Validators;

public class AddTransactionAttachmentValidator : AbstractValidator<AddTransactionAttachmentDto>
{
    public AddTransactionAttachmentValidator()
    {
        RuleFor(x => x.TransactionId)
            .NotEmpty().WithMessage("Transaction ID is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Attachment name is required")
            .MaximumLength(200).WithMessage("Attachment name cannot exceed 200 characters");

        RuleFor(x => x.FileType)
            .IsInEnum().When(x => x.FileType.HasValue).WithMessage("File type must be a valid attachment type");

        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required");
    }
}

public class UpdateTransactionAttachmentValidator : AbstractValidator<UpdateTransactionAttachmentDto>
{
    public UpdateTransactionAttachmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Attachment name is required")
            .MaximumLength(200).WithMessage("Attachment name cannot exceed 200 characters");

        RuleFor(x => x.FileType)
            .IsInEnum().When(x => x.FileType.HasValue).WithMessage("File type must be a valid attachment type");
    }
}
