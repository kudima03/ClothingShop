using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Categories.Queries.GetAll;

public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>> { }