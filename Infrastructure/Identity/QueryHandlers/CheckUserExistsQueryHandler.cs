using DomainServices.Services.OrdersService.Queries;
using Infrastructure.Identity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.QueryHandlers;

public class CheckUserExistsQueryHandler(IdentityContext.IdentityContext context) : IRequestHandler<CheckUserExistsQuery, bool>
{
    public async Task<bool> Handle(CheckUserExistsQuery request, CancellationToken cancellationToken)
    {
        User? user = await context.Users.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return user is not null;
    }
}