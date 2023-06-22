namespace Infrastructure.Identity.Constants;

public static class JwtSettings
{
    public const string Issuer = "LocalIdentity";
    public const string Audience = "Local";
    public const string SecretKey = "secret_key_for_jwt";
}