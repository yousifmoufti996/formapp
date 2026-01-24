using FormApp.Application.DTOs.Transactions;
using FormApp.Application.Interfaces;
using FormApp.Core.Entities;
using FormApp.Core.Exceptions;
using FormApp.Core.IRepositories;

namespace FormApp.Application.Services;

public class TransactionAttachmentService : ITransactionAttachmentService
{
    private readonly ITransactionAttachmentRepository _attachmentRepository;
    private readonly IUploadedFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionAttachmentService(
        ITransactionAttachmentRepository attachmentRepository,
        IUploadedFileRepository fileRepository,
        IFileService fileService,
        ITransactionRepository transactionRepository)
    {
        _attachmentRepository = attachmentRepository;
        _fileRepository = fileRepository;
        _fileService = fileService;
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<TransactionAttachmentResponseDto>> GetByTransactionRecordAsync(Guid transactionRecordId)
    {
        var attachments = await _attachmentRepository.GetByTransactionIdAsync(transactionRecordId);
        
        return attachments.Select(a => new TransactionAttachmentResponseDto
        {
            Id = a.Id,
            TransactionId = a.TransactionId,
            Name = a.Name,
            FileType = a.FileType,
            FileId = a.FileId,
            FileName = a.File.FileName,
            FileExtension = a.File.FileExtension,
            FileUrl = _fileService.GetFileUrl(a.File.FilePath),
            FileSize = a.File.FileSize,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            CreatedByName = a.CreatedBy != null ? $"{a.CreatedBy.FirstName} {a.CreatedBy.LastName}" : string.Empty
        });
    }

    public async Task<TransactionAttachmentResponseDto> GetByIdAsync(Guid id)
    {
        var attachment = await _attachmentRepository.GetByIdAsync(id);
        
        if (attachment == null)
            throw new NotFoundException($"Attachment with ID {id} not found");

        return new TransactionAttachmentResponseDto
        {
            Id = attachment.Id,
            TransactionId = attachment.TransactionId,
            Name = attachment.Name,
            FileType = attachment.FileType,
            FileId = attachment.FileId,
            FileName = attachment.File.FileName,
            FileExtension = attachment.File.FileExtension,
            FileUrl = _fileService.GetFileUrl(attachment.File.FilePath),
            FileSize = attachment.File.FileSize,
            CreatedAt = attachment.CreatedAt,
            UpdatedAt = attachment.UpdatedAt,
            CreatedByName = attachment.CreatedBy != null ? $"{attachment.CreatedBy.FirstName} {attachment.CreatedBy.LastName}" : string.Empty
        };
    }

    public async Task<IEnumerable<TransactionAttachmentResponseDto>> CreateMultipleImagesAsync(AddMultipleTransactionImagesDto dto, Guid currentUserId)
    {
        if (dto.Images == null || !dto.Images.Any())
            throw new BadRequestException("At least one image is required");

        // Validate Transaction exists
        var transactionExists = await _transactionRepository.ExistsAsync(dto.TransactionId);
        
        if (!transactionExists)
            throw new NotFoundException($"Transaction with ID {dto.TransactionId} not found");

        // Validate all files are images
        var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        foreach (var imageDto in dto.Images)
        {
            var extension = Path.GetExtension(imageDto.Image.FileName).ToLowerInvariant();
            if (!allowedImageExtensions.Contains(extension))
                throw new BadRequestException($"File {imageDto.Image.FileName} is not a valid image. Only jpg, jpeg, png, and gif are allowed.");
        }

        var createdAttachments = new List<TransactionAttachmentResponseDto>();

        foreach (var imageDto in dto.Images)
        {
            // Upload file
            var uploadedFile = await _fileService.UploadFileAsync(imageDto.Image, "transactions");
            var savedFile = await _fileRepository.CreateAsync(uploadedFile);

            // Create attachment
            var attachment = new TransactionAttachment
            {
                TransactionId = dto.TransactionId,
                Name = Path.GetFileNameWithoutExtension(imageDto.Image.FileName),
                FileType = imageDto.FileType,
                FileId = savedFile.Id,
                CreatedById = currentUserId
            };

            var created = await _attachmentRepository.AddAsync(attachment);

            createdAttachments.Add(new TransactionAttachmentResponseDto
            {
                Id = created.Id,
                TransactionId = created.TransactionId,
                Name = created.Name,
                FileType = created.FileType,
                FileId = created.FileId,
                FileName = created.File.FileName,
                FileExtension = created.File.FileExtension,
                FileUrl = _fileService.GetFileUrl(created.File.FilePath),
                FileSize = created.File.FileSize,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt,
                CreatedByName = created.CreatedBy != null ? $"{created.CreatedBy.FirstName} {created.CreatedBy.LastName}" : string.Empty
            });
        }

        return createdAttachments;
    }

    public async Task<TransactionAttachmentResponseDto> CreateAsync(AddTransactionAttachmentDto dto, Guid currentUserId)
    {
        if (dto.File == null)
            throw new BadRequestException("File is required");

        // Validate Transaction exists
        var transactionExists = await _transactionRepository.ExistsAsync(dto.TransactionId);
        
        if (!transactionExists)
            throw new NotFoundException($"Transaction with ID {dto.TransactionId} not found");

        // Upload file
        var uploadedFile = await _fileService.UploadFileAsync(dto.File, "transactions");
        var savedFile = await _fileRepository.CreateAsync(uploadedFile);

        // Create attachment
        var attachment = new TransactionAttachment
        {
            TransactionId = dto.TransactionId,
            Name = dto.Name,
            FileType = dto.FileType,
            FileId = savedFile.Id,
            CreatedById = currentUserId
        };

        var created = await _attachmentRepository.AddAsync(attachment);

        return new TransactionAttachmentResponseDto
        {
            Id = created.Id,
            TransactionId = created.TransactionId,
            Name = created.Name,
            FileType = created.FileType,
            FileId = created.FileId,
            FileName = created.File.FileName,
            FileExtension = created.File.FileExtension,
            FileUrl = _fileService.GetFileUrl(created.File.FilePath),
            FileSize = created.File.FileSize,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt,
            CreatedByName = created.CreatedBy != null ? $"{created.CreatedBy.FirstName} {created.CreatedBy.LastName}" : string.Empty
        };
    }

    public async Task<TransactionAttachmentResponseDto> UpdateAsync(Guid id, UpdateTransactionAttachmentDto dto, Guid currentUserId)
    {
        var attachment = await _attachmentRepository.GetByIdAsync(id);
        
        if (attachment == null)
            throw new NotFoundException($"Attachment with ID {id} not found");

        attachment.Name = dto.Name;
        attachment.FileType = dto.FileType;

        // If new file is provided, upload it and delete old one
        if (dto.File != null)
        {
            var oldFile = await _fileRepository.GetByIdAsync(attachment.FileId);
            
            var uploadedFile = await _fileService.UploadFileAsync(dto.File, "transactions");
            var savedFile = await _fileRepository.CreateAsync(uploadedFile);
            
            attachment.FileId = savedFile.Id;

            // Delete old file
            if (oldFile != null)
            {
                await _fileService.DeleteFileAsync(oldFile.FilePath);
                await _fileRepository.DeleteAsync(oldFile.Id);
            }
        }

        var updated = await _attachmentRepository.UpdateAsync(attachment);

        return new TransactionAttachmentResponseDto
        {
            Id = updated.Id,
            TransactionId = updated.TransactionId,
            Name = updated.Name,
            FileType = updated.FileType,
            FileId = updated.FileId,
            FileName = updated.File.FileName,
            FileExtension = updated.File.FileExtension,
            FileUrl = _fileService.GetFileUrl(updated.File.FilePath),
            FileSize = updated.File.FileSize,
            CreatedAt = updated.CreatedAt,
            UpdatedAt = updated.UpdatedAt,
            CreatedByName = updated.CreatedBy != null ? $"{updated.CreatedBy.FirstName} {updated.CreatedBy.LastName}" : string.Empty
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        var attachment = await _attachmentRepository.GetByIdAsync(id);
        
        if (attachment == null)
            throw new NotFoundException($"Attachment with ID {id} not found");

        var file = await _fileRepository.GetByIdAsync(attachment.FileId);
        
        await _attachmentRepository.DeleteAsync(id);

        // Delete file
        if (file != null)
        {
            await _fileService.DeleteFileAsync(file.FilePath);
            await _fileRepository.DeleteAsync(file.Id);
        }
    }
}
