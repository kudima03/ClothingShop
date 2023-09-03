namespace Infrastructure.Identity.Constants;

public class JwtSettings
{
    public string Issuer { get; init; } = null!;

    public string Audience { get; init; } = null!;

    public string SecretKey { get; init; } = null!;

    public long TokenLifetimeMinutes { get; init; } = 120;
}