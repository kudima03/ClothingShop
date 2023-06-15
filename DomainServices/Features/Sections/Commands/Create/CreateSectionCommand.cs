using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Sections.Commands.Create;

public class CreateSectionCommand : IRequest<Section>
{
    public string Name { get; init; }
}