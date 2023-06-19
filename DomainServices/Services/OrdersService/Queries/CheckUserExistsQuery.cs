using MediatR;

namespace DomainServices.Services.OrdersService.Queries;
public class CheckUserExistsQuery : IRequest<bool>
{
    public long Id { get; init; }

    public CheckUserExistsQuery(long id)
    {
        Id = id;
    }
}
