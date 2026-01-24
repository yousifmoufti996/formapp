using System.Security.Cryptography;

namespace FormApp.Helper.Utilities;

public static class TokenGenerator
{
    public static string GenerateRandomToken(int length = 32)
    {
        var randomBytes = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
