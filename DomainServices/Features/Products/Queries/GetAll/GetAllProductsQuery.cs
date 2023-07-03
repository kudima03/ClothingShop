using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetAll;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>> { }