﻿using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;

namespace DomainServices.Features.Users.Validators;

public class UpdateUserCommandValidator : AbstractValidator<UpdateCommand<User>>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Entity.Email)} cannot be null or empty");

        RuleFor(x => x.Entity.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Entity.Password)} cannot be null or empty");

        RuleFor(x => x.Entity.UserTypeId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.UserTypeId)} must be greater than 0.");
    }
}