using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Subcategories.Queries.GetAll;

public class GetAllSubcategoriesQuery : IRequest<IEnumerable<Subcategory>>;