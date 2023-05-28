using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.ProductsOptions.Queries;
public class GetAllProductsOptionsQuery : EntityCollectionQuery<ProductOption>
{
    public GetAllProductsOptionsQuery()
        : base(new Specification<ProductOption, ProductOption>(selector: x => x))
    { }
}
