using ApplicationCore.Exceptions;
using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Infrastructure.Identity.Services;

public class AuthorizationService(ITokenClaimsService tokenClaimsService,
                                  UserManager<User> userManager,
                                  IHttpContextAccessor contextAccessor,
                                  IOptions<JwtSettings> settings)
    : IAuthorizationService
{
    private readonly JwtSettings _jwtSettings = settings.Value;

    public async Task SignInAsync(string email, string password)
    {
        User? user = await userManager.FindByEmailAsync(email);

        if (user is null || user.DeletionDateTime is not null)
        {
            throw new AuthorizationException("Invalid login or password.");
        }

        bool passwordCorrect = await userManager.CheckPasswordAsync(user, password);

        if (!passwordCorrect)
        {
            throw new AuthorizationException("Invalid login or password.");
        }

        string token = await tokenClaimsService.GetTokenAsync(user.Email);

        contextAccessor.HttpContext?.Response.Cookies.Append
            (JwtConstants.TokenType,
             token,
             new CookieOptions
             {
                 Expires =
                     DateTimeOffset.Now.AddMinutes(_jwtSettings.TokenLifetimeMinutes)
             });
    }

    public async Task<long> RegisterAsync(User user)
    {
        IdentityResult result = await userManager.CreateAsync(user, user.PasswordHash);

        if (!result.Succeeded)
        {
            throw new AuthorizationException
                ($"Unable to register such user. Reasons: {string.Join(',', result.Errors.Select(x => x.Description))}");
        }

        await userManager.AddToRoleAsync(user, RoleName.Customer);

        return user.Id;
    }

    public Task SingOutAsync()
    {
        contextAccessor.HttpContext?.Response.Cookies.Delete(JwtConstants.TokenType);

        return Task.CompletedTask;
    }
}