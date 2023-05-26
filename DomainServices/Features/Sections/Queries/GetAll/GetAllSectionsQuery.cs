using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Sections.Queries.GetAll;

public class GetAllSectionsQuery : IRequest<IEnumerable<Section>>
{
}