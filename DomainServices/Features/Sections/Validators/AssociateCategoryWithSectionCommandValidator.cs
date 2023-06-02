﻿using DomainServices.Features.Sections.Commands.AssociateCategory;
using FluentValidation;

namespace DomainServices.Features.Sections.Validators;

public class AssociateCategoryWithSectionCommandValidator : AbstractValidator<AssociateCategoryWithSectionCommand>
{
    public AssociateCategoryWithSectionCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage(x => "Object must be not null");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.CategoryId)} must be greater than 0");

        RuleFor(x => x.SectionId)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.SectionId)} must be greater than 0");
    }
}