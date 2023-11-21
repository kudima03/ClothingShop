using MediatR;

namespace DomainServices.Services.OrdersService.Queries;

public class CheckUserExistsQuery(long id) : IRequest<bool>
{
    public long Id { get; init; } = id;
}