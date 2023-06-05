using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Colors.Commands.Update;

public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, Unit>
{
    private readonly IRepository<Color> _colorsRepository;

    public UpdateColorCommandHandler(IRepository<Color> colorsRepository)
    {
        _colorsRepository = colorsRepository;
    }

    public async Task<Unit> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
    {
        Color? color = await _colorsRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Color.Id,
            cancellationToken: cancellationToken);

        if (color is null)
        {
            throw new EntityNotFoundException($"{nameof(Color)} with id:{request.Color.Id} doesn't exist.");
        }

        color.Hex = request.Color.Hex;
        color.Name = request.Color.Name;
        await _colorsRepository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}