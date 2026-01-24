namespace FormApp.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string username, string email);
    string GenerateRefreshToken();
    Guid? ValidateToken(string token);
}
