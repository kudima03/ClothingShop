using MediatR;

namespace DomainServices.Features.Brands.Commands.Update;

public class UpdateBrandCommand : IRequest<Unit>
{
    public UpdateBrandCommand(long id, string name)
    {
        Name = name;
        Id = id;
    }

    public long Id { get; }
    public string Name { get; }
}