﻿using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.Products.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateCommand<Product>>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Entity.Name)} cannot be null or empty");

        RuleFor(x => x.Entity.BrandId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.BrandId)} must be greater than 0.");

        RuleFor(x => x.Entity.SubcategoryId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.SubcategoryId)} must be greater than 0.");

        RuleFor(x => x.Entity.Reviews)
            .Empty()
            .WithMessage(x =>
                $"When creating new {x.Entity.GetType().Name},{nameof(x.Entity.Reviews)} must be empty. Associate in another request");
    }
}