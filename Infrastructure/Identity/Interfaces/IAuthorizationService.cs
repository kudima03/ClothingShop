using Infrastructure.Identity.Entity;

namespace Infrastructure.Identity.Interfaces;

public interface IAuthorizationService
{
    Task SignInAsync(string email, string password);

    Task<long> RegisterAsync(User user);

    Task SingOutAsync();
}