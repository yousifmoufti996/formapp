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
    /// Get all facility records
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var records = await _service.GetAllAsync();
        return Ok(records);
    }

    /// <summary>
    /// Get facility record by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var record = await _service.GetByIdAsync(id);
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
