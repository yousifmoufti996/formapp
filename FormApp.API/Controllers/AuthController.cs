using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FormApp.Application.DTOs.Auth;
using FormApp.Application.Interfaces;
using FormApp.Core.Exceptions;
using FormApp.API.Attributes;

namespace FormApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Authenticates a user and returns a JWT and refresh token.
    /// </summary>
    /// <param name="dto">
    /// Login credentials.<br></br>
    /// <b>Email</b>: Required, max length 255, must be a valid email address.<br></br>
    /// <b>Password</b>: Required, min length 6, must contain at least one special character.<br></br>
    /// <b>FcmToken</b>: Optional. Firebase Cloud Messaging token for push notifications.<br></br>
    /// </param>
    /// <returns>JWT access token and refresh token.</returns>
    [AllowAnonymous]
    [AuthRateLimit]
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), 200)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        // Extract IP address and device info
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var deviceInfo = HttpContext.Request.Headers["User-Agent"].ToString();
        if (string.IsNullOrEmpty(deviceInfo))
            deviceInfo = "Unknown Device";

        var result = await _authService.Login(dto, ipAddress, deviceInfo);
        return Ok(result);
    }

    /// <summary>
    /// Registers a new user account. Requires authentication.
    /// </summary>
    /// <param name="request">
    /// Registration details.<br></br>
    /// <b>Username</b>: Optional. If not provided, email will be used as username.<br></br>
    /// <b>Email</b>: Required, must be a valid email address.<br></br>
    /// <b>Password</b>: Required, minimum 6 characters, must contain at least one special character, digit, uppercase and lowercase letter.<br></br>
    /// <b>FirstName</b>: Optional.<br></br>
    /// <b>LastName</b>: Optional.
    /// </param>
    /// <returns>JWT access token and refresh token.</returns>
    /// <response code="200">Returns the access and refresh tokens.</response>
    /// <response code="400">Invalid request or user already exists.</response>
    /// <response code="401">User not authenticated.</response>
    [TokenAuthorization]
    [AuthRateLimit]
    [HttpPost("register")]
    [ProducesResponseType(typeof(LoginResponseDto), 200)]
    [ProducesResponseType(typeof(ExceptionModel), 400)]
    [ProducesResponseType(typeof(ExceptionModel), 401)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var result = await _authService.RegisterAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Exchanges a valid refresh token for a new access token.
    /// </summary>
    /// <param name="request">
    /// Refresh token payload.<br></br>
    /// <b>RefreshToken</b>: Required.
    /// </param>
    /// <returns>New access and refresh tokens.</returns>
    /// <response code="200">Returns new tokens.</response>
    /// <response code="400">Invalid request payload.</response>
    /// <response code="401">Invalid or expired refresh token.</response>
    [AllowAnonymous]
    [AuthRateLimit]
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(LoginResponseDto), 200)]
    [ProducesResponseType(typeof(ExceptionModel), 400)]
    [ProducesResponseType(typeof(ExceptionModel), 401)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var result = await _authService.RefreshTokenAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Revokes the given refresh token and optionally deletes the associated device token.
    /// </summary>
    /// <param name="dto">
    /// Refresh token to revoke.<br></br>
    /// <b>RefreshToken</b>: Required.<br></br>
    /// <b>FcmToken</b>: Optional. If provided, only the device token matching this FCM token will be deleted.
    /// </param>
    /// <response code="204">Token revoked.</response>
    /// <response code="401">Invalid refresh token.</response>
    [AllowAnonymous]
    [AuthRateLimit]
    [HttpPost("revoke-token")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ExceptionModel), 401)]
    public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenRequestDto dto)
    {
        await _authService.RevokeToken(dto.RefreshToken, dto.FcmToken);
        return NoContent();
    }
}
