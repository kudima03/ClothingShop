using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IRepository<CustomerInfo> _customersRepository;
    private readonly IRepository<User> _userRepository;

    public CreateUserCommandHandler(IRepository<User> userRepository, IRepository<CustomerInfo> customersRepository)
    {
        _userRepository = userRepository;
        _customersRepository = customersRepository;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await ValidateEmailAsync(request.Email, cancellationToken);

        User newUser = new()
        {
            Email = request.Email,
            Password = request.Password,
            UserTypeId = request.UserTypeId
        };

        CustomerInfo newCustomerInfo = new()
        {
            Name = request.Name,
            Surname = request.Surname,
            Patronymic = request.Patronymic,
            Phone = request.Phone,
            Address = request.Address,
            User = newUser
        };

        try
        {
            CustomerInfo? customerInfo = await _customersRepository.InsertAsync(newCustomerInfo, cancellationToken);
            await _customersRepository.SaveChangesAsync(cancellationToken);
            return customerInfo.User;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(User)} operation. Check input.");
        }
    }

    private async Task ValidateEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        bool emailExists = await _userRepository.ExistsAsync(x => x.Email == email, cancellationToken);
        if (emailExists)
        {
            throw new ValidationException(new[] { new ValidationFailure("Brand.Name", "Such brand already exists!") });
        }
    }
}