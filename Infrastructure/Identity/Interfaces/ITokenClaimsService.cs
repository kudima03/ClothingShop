namespace Infrastructure.Identity.Interfaces;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userEmail);
}