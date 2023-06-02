﻿using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Delete;
using FluentValidation;

namespace DomainServices.Features.ProductsOptions.Validators;

public class DeleteProductOptionCommandValidator : AbstractValidator<DeleteCommand<ProductOption>>
{
    public DeleteProductOptionCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.Id)} must be greater than 0.");
    }
}