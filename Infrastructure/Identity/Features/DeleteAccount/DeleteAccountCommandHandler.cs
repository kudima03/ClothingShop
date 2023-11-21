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

public class DeleteAccountCommandHandler(UserManager<User> userManager,
                                         IHttpContextAccessor contextAccessor,
                                         IRepository<Review> reviewsRepository,
                                         IRepository<Order> ordersRepository,
                                         IRepository<OrderStatus> orderStatusesRepository)
    : IRequestHandler<DeleteAccountCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        //Don't use cancellationToken here because of transaction consistence on 3 separate repositories.
        ClaimsIdentity? claimsIdentity = contextAccessor.HttpContext?.User.Identity as ClaimsIdentity;

        string email = claimsIdentity.FindFirst(ClaimTypes.Email).Value;

        User? user = await userManager.FindByEmailAsync(email);

        IList<Review>? reviewsToRemove = await
                                             reviewsRepository.GetAllAsync(predicate: x => x.UserId == user.Id);

        reviewsRepository.Delete(reviewsToRemove);

        await reviewsRepository.SaveChangesAsync();

        IList<Order>? ordersToCancel = await
                                           ordersRepository.GetAllAsync(predicate: x => x.UserId == user.Id);

        OrderStatus? cancelledOrderStatus =
            await orderStatusesRepository.GetFirstOrDefaultAsync(predicate: x => x.Name == OrderStatusName.Cancelled);

        foreach (Order? item in ordersToCancel)
        {
            item.OrderStatus = cancelledOrderStatus;
        }

        await ordersRepository.SaveChangesAsync();

        user.Delete();

        await userManager.UpdateAsync(user);

        contextAccessor.HttpContext?.Response.Cookies.Delete(JwtConstants.TokenType);

        return Unit.Value;
    }
}