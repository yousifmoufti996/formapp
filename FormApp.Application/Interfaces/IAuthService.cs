using FormApp.Application.DTOs.Auth;

namespace FormApp.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginRequestDto dto, string ipAddress, string deviceInfo);
    Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task<LoginResponseDto> RegisterAsync(RegisterRequestDto request);
    Task RevokeToken(string refreshToken, string? fcmToken = null);
}
