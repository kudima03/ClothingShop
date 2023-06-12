using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<Brand>
{
    public string Name { get; init; }
}