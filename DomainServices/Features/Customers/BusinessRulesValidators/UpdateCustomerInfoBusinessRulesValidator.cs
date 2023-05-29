﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.BusinessRulesValidators;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;
using FluentValidation.Results;

namespace DomainServices.Features.Customers.BusinessRulesValidators;

public class UpdateCustomerInfoBusinessRulesValidator : IBusinessRulesValidator<UpdateCommand<CustomerInfo>>
{
    private readonly IRepository<User> _usersRepository;

    public UpdateCustomerInfoBusinessRulesValidator(IRepository<User> usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task ValidateAsync(UpdateCommand<CustomerInfo> entity, CancellationToken cancellation = default)
    {
        if (!await _usersRepository.ExistsAsync(x => x.Id == entity.Entity.UserId, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.UserId)}",
                    $"{entity.Entity.User.GetType().Name} with id: {entity.Entity.UserId} doesn't exist")
            });
        }
    }
}