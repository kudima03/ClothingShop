using Infrastructure.Identity.Constants;
using Microsoft.AspNetCore.SignalR;

namespace Web.SignalR;

public class SignalRUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User.Claims.Single(claim => claim.Type == CustomClaimName.Id).Value;
    }
}