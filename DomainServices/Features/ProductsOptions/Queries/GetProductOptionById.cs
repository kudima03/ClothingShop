using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using ApplicationCore.Specifications.ProductOption;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.ProductsOptions.Queries;
public class GetProductOptionById : SingleEntityQuery<ProductOption>
{
    public long Id { get; init; }
    public GetProductOptionById(long id) 
        : base(new ProductOptionWithColorAndImages(predicate: x=>x.Id == id))
    {
        Id = id;
    }
}
