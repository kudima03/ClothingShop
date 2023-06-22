using ApplicationCore.Exceptions;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity.Services;
public class IdentityTokenClaimsService : ITokenClaimsService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public IdentityTokenClaimsService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> GetTokenAsync(string userEmail)
    {
        User? user = await _userManager.FindByEmailAsync(userEmail);
        if (user is null) throw new EntityNotFoundException($"User with email {userEmail} doesn't exist.");
        IList<string> roles = await _userManager.GetRolesAsync(user);

        List<Claim> claims = new List<Claim>
        {
            new Claim(CustomClaimName.Id, user.Id.ToString())
        };

        foreach (string role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        JwtSecurityToken token =
            new JwtSecurityToken(JwtSettings.Issuer,
                   JwtSettings.Audience,
                   claims: claims,
                   notBefore: DateTime.Now,
                   expires: DateTime.Now.AddMinutes(_configuration.GetValue("TokenLifetimeMinutes", 120)),
                   new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey)), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
