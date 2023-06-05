using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Colors.Queries.GetAll;

public class GetAllColorsQuery : IRequest<IEnumerable<Color>>
{
}