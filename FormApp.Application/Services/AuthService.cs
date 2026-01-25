using FormApp.Application.DTOs.Auth;
using FormApp.Application.Interfaces;
using FormApp.Core.Exceptions;
using FormApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FormApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<User> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto dto, string ipAddress, string deviceInfo)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            throw new UnauthorizedException("Auth.InvalidCredentials");

        if (string.IsNullOrWhiteSpace(user.Email))
            throw new UnauthorizedException("Auth.InvalidCredentials");

        var roles = await _userManager.GetRolesAsync(user);
        var token = GenerateJwtToken(user, roles);
        var refreshToken = GenerateRefreshToken();

        // Update user's last login
        user.LastLoginAt = DateTime.UtcNow;
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30);
        await _userManager.UpdateAsync(user);

        return new LoginResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            Name = user.UserName,
            Roles = roles
        };
    }

    private string GenerateJwtToken(User user, IList<string> roles)
    {
        var jwtSettings = _config.GetSection("Jwt");
        var secretKey = jwtSettings["SecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var expiresMinutes = int.TryParse(jwtSettings["ExpiresMinutes"], out var min) ? min : 60;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jti = Guid.NewGuid().ToString();
        var issuedAt = DateTime.UtcNow;
        var expiresAt = issuedAt.AddMinutes(expiresMinutes);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, jti),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(issuedAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim("UserName", user.UserName ?? "")
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            notBefore: issuedAt,
            expires: expiresAt,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        // Find user by refresh token
        var user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == request.RefreshToken);
        if (user == null)
            throw new UnauthorizedException("Invalid refresh token");

        // Check if refresh token has expired
        if (user.RefreshTokenExpiryTime == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            throw new UnauthorizedException("Refresh token has expired");

        // Generate new tokens
        var roles = await _userManager.GetRolesAsync(user);
        var newToken = GenerateJwtToken(user, roles);
        var newRefreshToken = GenerateRefreshToken();

        // Update user's refresh token
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30);
        await _userManager.UpdateAsync(user);

        return new LoginResponseDto
        {
            Token = newToken,
            RefreshToken = newRefreshToken,
            Name = user.UserName,
            Roles = roles
        };
    }

    public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        // Check if email already exists (primary unique identifier)
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new BadRequestException("Email already exists");
        }

        // Use email as username if username is not provided
        var username = string.IsNullOrEmpty(request.Username) ? request.Email : request.Username;

        // Check if username exists (only if different from email)
        if (username != request.Email)
        {
            var existingByUsername = await _userManager.FindByNameAsync(username);
            if (existingByUsername != null)
            {
                throw new BadRequestException("Username already exists");
            }
        }

        // Create new user
        var user = new User
        {
            UserName = username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new BadRequestException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Generate tokens
        var roles = await _userManager.GetRolesAsync(user);
        var token = GenerateJwtToken(user, roles);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        return new LoginResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            Name = user.UserName,
            Roles = roles
        };
    }

    public async Task RevokeToken(string refreshTokenValue, string? fcmToken = null)
    {
        // Find user by refresh token
        var user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshTokenValue);
        if (user == null)
            throw new UnauthorizedException("Invalid refresh token");

        // Clear refresh token
        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _userManager.UpdateAsync(user);
    }
}
