using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Infrastructure.Identity.Features.DeleteAccount;
public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly UserManager<User> _userManager;

    private readonly IHttpContextAccessor _contextAccessor;

    private readonly IRepository<Review> _reviewsRepository;

    private readonly IRepository<Order> _ordersRepository;

    private readonly IRepository<OrderStatus> _orderStatusesRepository;

    public DeleteAccountCommandHandler(UserManager<User> userManager,
                                       IHttpContextAccessor contextAccessor,
                                       IRepository<Review> reviewsRepository,
                                       IRepository<Order> ordersRepository,
                                       IRepository<OrderStatus> orderStatusesRepository)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
        _reviewsRepository = reviewsRepository;
        _ordersRepository = ordersRepository;
        _orderStatusesRepository = orderStatusesRepository;
    }

    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        ClaimsIdentity? claimsIdentity = _contextAccessor.HttpContext?.User.Identity as ClaimsIdentity;

        string email = claimsIdentity.FindFirst(ClaimTypes.Email).Value;

        User? user = await _userManager.FindByEmailAsync(email);

        IList<Review>? reviewsToRemove = await 
                                             _reviewsRepository.GetAllAsync(predicate: x => x.UserId == user.Id,
                                                                              cancellationToken: cancellationToken);
        _reviewsRepository.Delete(reviewsToRemove);

        await _reviewsRepository.SaveChangesAsync(cancellationToken);

        IList<Order>? ordersToCancel = await 
                                           _ordersRepository.GetAllAsync(predicate: x => x.UserId == user.Id, cancellationToken: cancellationToken);

        OrderStatus? cancelledOrderStatus =
            await _orderStatusesRepository.GetFirstOrDefaultAsync(predicate: x => x.Name == OrderStatusName.Cancelled,
                                                                  cancellationToken: cancellationToken);

        foreach (Order? item in ordersToCancel)
        {
            item.OrderStatus = cancelledOrderStatus;
        }

        await _ordersRepository.SaveChangesAsync(cancellationToken);

        user.Delete();

        await _userManager.UpdateAsync(user);

        _contextAccessor.HttpContext?.Response.Cookies.Delete(JwtConstants.TokenType);

        return Unit.Value;
    }
}