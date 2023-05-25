using ApplicationCore.Entities;
using MediatR;

namespace Web.Features.Brands;

public class GetAllBrands : IRequest<IEnumerable<Brand>>
{
}