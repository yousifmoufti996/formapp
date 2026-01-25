using FormApp.API.Attributes;
using FormApp.Application.DTOs.Transactions;
using FormApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FormApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[TokenAuthorization]
public class TransactionRecordsController : ControllerBase
{
    private readonly ITransactionRecordService _service;

    public TransactionRecordsController(ITransactionRecordService service)
    {
        _service = service;
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
    }

    /// <summary>
    /// Get all facility records created by the current user
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var currentUserId = GetCurrentUserId();
        var records = await _service.GetAllAsync(currentUserId);
        return Ok(records);
    }

    /// <summary>
    /// Get facility record by ID (only if created by current user)
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var currentUserId = GetCurrentUserId();
        var record = await _service.GetByIdAsync(id, currentUserId);
        return Ok(record);
    }

    /// <summary>
    /// Create a new facility record
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionRecordDto dto)
    {
        try
        {
            var currentUserId = GetCurrentUserId();
            var record = await _service.CreateAsync(dto, currentUserId);
            return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
        {
            // Handle database-level errors like truncation, constraint violations, etc.
            return BadRequest(new { error = "Database error: " + ex.InnerException?.Message ?? ex.Message });
        }
    }

    /// <summary>
    /// Update an existing facility record
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateTransactionRecordDto dto)
    {
        try
        {
            var record = await _service.UpdateAsync(id, dto);
            return Ok(record);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
        {
            // Handle database-level errors like truncation, constraint violations, etc.
            return BadRequest(new { error = "Database error: " + ex.InnerException?.Message ?? ex.Message });
        }
    }

    /// <summary>
    /// Delete a facility record
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
