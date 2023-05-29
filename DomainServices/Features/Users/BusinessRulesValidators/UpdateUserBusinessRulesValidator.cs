using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.BusinessRulesValidators;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;
using FluentValidation.Results;

namespace DomainServices.Features.Users.BusinessRulesValidators;

public class UpdateUserBusinessRulesValidator : IBusinessRulesValidator<UpdateCommand<User>>
{
    private readonly IRepository<User> _usersRepository;

    private readonly IRepository<UserType> _userTypesRepository;

    public UpdateUserBusinessRulesValidator(IRepository<User> usersRepository,
        IRepository<UserType> userTypesRepository)
    {
        _usersRepository = usersRepository;
        _userTypesRepository = userTypesRepository;
    }

    public async Task ValidateAsync(UpdateCommand<User> entity, CancellationToken cancellation = default)
    {
        if (!await _userTypesRepository.ExistsAsync(x => x.Id == entity.Entity.UserTypeId, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.UserTypeId)}",
                    $"{entity.Entity.UserType.GetType().Name} with id: {entity.Entity.UserTypeId} doesn't exist")
            });
        }

        if (!await _usersRepository.ExistsAsync(x => x.Email == entity.Entity.Email, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.Email)}",
                    $"{nameof(entity.Entity.Email)} already exists.")
            });
        }
    }
}