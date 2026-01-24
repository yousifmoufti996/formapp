namespace FormApp.Application.DTOs.Auth;

public class LoginRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? FcmToken { get; set; }
}

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string? Name { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}

public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
    public string? FcmToken { get; set; }
}

public class RegisterRequestDto
{
    public string? Username { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
