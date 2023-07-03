using ApplicationCore.Entities;
using DomainServices.Features.ShoppingCarts.Commands.Update;
using DomainServices.Features.ShoppingCarts.Queries.GetByUserId;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.Customer)]
public class ShoppingCartsController : Controller
{
    private readonly IMediator _mediator;

    public ShoppingCartsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<ShoppingCart>> GetUserShoppingCartData()
    {
        long id = long.Parse(User.Claims.Single(x => x.Type == CustomClaimName.Id).Value);
        GetShoppingCartByUserIdQuery query = new GetShoppingCartByUserIdQuery(id);
        ShoppingCart shoppingCart = await _mediator.Send(query);

        return Ok(shoppingCart);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<ShoppingCart>> GetUserShoppingCartView()
    {
        long id = long.Parse(User.Claims.Single(x => x.Type == CustomClaimName.Id).Value);
        GetShoppingCartByUserIdQuery query = new GetShoppingCartByUserIdQuery(id);
        ShoppingCart shoppingCart = await _mediator.Send(query);

        return View("ShoppingCart", shoppingCart);
    }

    [HttpPut]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<ShoppingCart>> UpdateShoppingCart([FromBody] UpdateShoppingCartCommand command)
    {
        long userId = long.Parse(User.Claims.Single(x => x.Type == CustomClaimName.Id).Value);
        command.UserId = userId;
        await _mediator.Send(command);

        return Ok();
    }
}