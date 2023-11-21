using MediatR;

namespace DomainServices.Features.Brands.Commands.Delete;

public class DeleteBrandCommand(long id) : IRequest<Unit>
{
    public long Id { get; init; } = id;
}