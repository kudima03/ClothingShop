using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.BusinessRulesValidators;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;
using FluentValidation.Results;

namespace DomainServices.Features.Reviews.BusinessRulesValidators;

public class CreateReviewBusinessRulesValidator : IBusinessRulesValidator<CreateCommand<Review>>
{
    private readonly IRepository<Product> _productsRepository;

    private readonly IRepository<User> _usersRepository;

    public CreateReviewBusinessRulesValidator(IRepository<Product> productsRepository,
        IRepository<User> usersRepository)
    {
        _productsRepository = productsRepository;
        _usersRepository = usersRepository;
    }

    public async Task ValidateAsync(CreateCommand<Review> entity, CancellationToken cancellation = default)
    {
        if (!await _productsRepository.ExistsAsync(x => x.Id == entity.Entity.ProductId, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.ProductId)}",
                    $"{entity.Entity.Product.GetType().Name} with id: {entity.Entity.ProductId} doesn't exist")
            });
        }

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