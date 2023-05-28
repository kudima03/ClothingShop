using DomainServices.Features.ProductsOptions.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.ProductsOptions.Validators;
public class GetProductOptionByIdQueryValidator : AbstractValidator<GetProductOptionById>
{
    public GetProductOptionByIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.Id)} must be greater than 0.");
    }
}
