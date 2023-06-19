using ApplicationCore.Exceptions;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Infrastructure.Identity.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly ITokenClaimsService _tokenClaimsService;

    private readonly UserManager<User> _userManager;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IConfiguration _configuration;

    public AuthorizationService(ITokenClaimsService tokenClaimsService,
                                UserManager<User> userManager,
                                IHttpContextAccessor contextAccessor,
                                IConfiguration configuration)
    {
        _tokenClaimsService = tokenClaimsService;
        _userManager = userManager;
        _httpContextAccessor = contextAccessor;
        _configuration = configuration;
    }

    public async Task SignInAsync(string email, string password)
    {
        User? user = await _userManager.FindByEmailAsync(email);

        if (user is null || user.DeletionDateTime is not null)
        {
            throw new AuthorizationException("Invalid login or password.");
        }

        bool passwordCorrect = await _userManager.CheckPasswordAsync(user, password);

        if (!passwordCorrect)
        {
            throw new AuthorizationException("Invalid login or password.");
        }

        string token = await _tokenClaimsService.GetTokenAsync(user.Email);

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(JwtConstants.TokenType, token, new CookieOptions()
        {
            Expires = DateTimeOffset.Now.AddMinutes(_configuration.GetValue("TokenLifetimeMinutes", 120))
        });
    }

    public async Task RegisterAsync(User user)
    {
        IdentityResult result = await _userManager.CreateAsync(user, user.PasswordHash);

        if (!result.Succeeded)
        {
            throw new AuthorizationException($"Unable to register such user. Reasons: {string.Join(',', result.Errors.Select(x => x.Description))}");
        }

        await _userManager.AddToRoleAsync(user, RoleName.Customer);
    }

    public Task SingOutAsync()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(JwtConstants.TokenType);
        return Task.CompletedTask;
    }
}
