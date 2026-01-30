using FormApp.API.Attributes;
using FormApp.Application.DTOs.Common;
using FormApp.Application.DTOs.Transactions;
using FormApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FormApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[TokenAuthorization]
public class TransactionAttachmentsController : ControllerBase
{
    private readonly ITransactionAttachmentService _attachmentService;

    public TransactionAttachmentsController(ITransactionAttachmentService attachmentService)
    {
        _attachmentService = attachmentService;
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
    }

    /// <summary>
    /// Get all attachments for a transaction record
    /// </summary>
    [HttpGet("transaction/{transactionId}")]
    public async Task<IActionResult> GetByTransactionRecord(Guid transactionId, [FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null)
    {
        var pagination = pageNumber.HasValue && pageSize.HasValue 
            ? new PaginationRequestDto(pageNumber.Value, pageSize.Value) 
            : null;
        var attachments = await _attachmentService.GetByTransactionRecordAsync(transactionId, pagination);
        return Ok(attachments);
    }

    /// <summary>
    /// Get attachment by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var attachment = await _attachmentService.GetByIdAsync(id);
        return Ok(attachment);
    }

    /// <summary>
    /// Create a new transaction attachment
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddTransactionAttachmentDto dto)
    {
        var currentUserId = GetCurrentUserId();
        var attachment = await _attachmentService.CreateAsync(dto, currentUserId);
        return CreatedAtAction(nameof(GetById), new { id = attachment.Id }, attachment);
    }

    /// <summary>
    /// Upload multiple images for a transaction record
    /// </summary>
    [HttpPost("upload-images")]
    public async Task<IActionResult> UploadImages([FromForm] AddMultipleTransactionImagesDto dto)
    {
        var currentUserId = GetCurrentUserId();
        var attachments = await _attachmentService.CreateMultipleImagesAsync(dto, currentUserId);
        return Ok(attachments);
    }

    /// <summary>
    /// Update an existing transaction attachment
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateTransactionAttachmentDto dto)
    {
        var currentUserId = GetCurrentUserId();
        var attachment = await _attachmentService.UpdateAsync(id, dto, currentUserId);
        return Ok(attachment);
    }

    /// <summary>
    /// Delete a transaction attachment
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _attachmentService.DeleteAsync(id);
        return NoContent();
    }
}
