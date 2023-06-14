using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Brands.Queries.GetAll;

public class GetAllBrandsQuery : IRequest<IEnumerable<Brand>> { }