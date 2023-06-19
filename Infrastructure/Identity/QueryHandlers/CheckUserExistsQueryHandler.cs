using DomainServices.Services.OrdersService.Queries;
using Infrastructure.Identity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.QueryHandlers;
public class CheckUserExistsQueryHandler : IRequestHandler<CheckUserExistsQuery, bool>
{
    private readonly IdentityContext.IdentityContext _context;

    public CheckUserExistsQueryHandler(IdentityContext.IdentityContext context) 
    {
        _context = context;
    }

    public async Task<bool> Handle(CheckUserExistsQuery request, CancellationToken cancellationToken)
    {
        User? user = await _context.Users.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return user is not null;
    }
}
