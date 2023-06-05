using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Colors.Queries.GetAll;

public class GetAllColorsQueryHandler : IRequestHandler<GetAllColorsQuery, IEnumerable<Color>>
{
    private readonly IReadOnlyRepository<Color> _colorsRepository;

    public GetAllColorsQueryHandler(IReadOnlyRepository<Color> colorsRepository)
    {
        _colorsRepository = colorsRepository;
    }

    public async Task<IEnumerable<Color>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
    {
        return await _colorsRepository.GetAllAsync(cancellationToken);
    }
}