using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.Interfaces;
using MediatR;

namespace Infrastructure.Identity.Features.Register;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IAuthorizationService _authorizationService;

    private readonly IRepository<ShoppingCart> _shoppingCartsRepository;

    public RegisterCommandHandler(IAuthorizationService authorizationService,
        IRepository<ShoppingCart> shoppingCartsRepository)
    {
        _authorizationService = authorizationService;
        _shoppingCartsRepository = shoppingCartsRepository;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        User user = new User()
        {
            UserName = request.Email,
            PasswordHash = request.Password,
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            Patronymic = request.Patronymic,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
        };

        long userId = await _authorizationService.RegisterAsync(user);

        ShoppingCart? newShoppingCart = new ShoppingCart()
        {
            UserId = userId
        };

        await _shoppingCartsRepository.InsertAsync(newShoppingCart, cancellationToken);

        await _shoppingCartsRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
