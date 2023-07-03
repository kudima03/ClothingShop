using MediatR;

namespace DomainServices.Services.OrdersService.Queries;

public class CheckUserExistsQuery : IRequest<bool>
{
    public CheckUserExistsQuery(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}