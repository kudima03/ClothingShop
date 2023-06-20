using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Web.Middlewares;

public class JwtSubstitutionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string? token = context.Request.Cookies[JwtConstants.TokenType];

        if (token is not null)
        {
            context.Request.Headers.Authorization = new StringValues($"Bearer {token}");
        }

        await next(context);
    }
}
