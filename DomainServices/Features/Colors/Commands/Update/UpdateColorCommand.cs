using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Colors.Commands.Update;

public class UpdateColorCommand : IRequest<Unit>
{
    public UpdateColorCommand(Color color)
    {
        Color = color;
    }

    public Color Color { get; init; }
}