using MediatR;

namespace DomainServices.Features.Brands.Commands.Update;

public class UpdateBrandCommand : IRequest<Unit>
{
    public long Id { get; init; }

    public string Name { get; init; }
}