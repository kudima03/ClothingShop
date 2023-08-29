﻿using ApplicationCore.Entities;
using DomainServices.Features.Orders.Commands.Cancel;
using DomainServices.Features.Orders.Commands.Create;
using DomainServices.Features.Orders.Queries.GetAll;
using DomainServices.Features.Orders.Queries.GetById;
using DomainServices.Features.Orders.Queries.GetByUserId;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.Customer)]
public class OrdersController : Controller
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = PolicyName.Administrator)]
    [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Order>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllOrdersQuery query = new GetAllOrdersQuery();
        IEnumerable<Order> orders = await _mediator.Send(query, cancellationToken);

        return Ok(orders);
    }

    [HttpGet("{id:long}")]
    [Authorize(Policy = PolicyName.Administrator)]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Order>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetOrderByIdQuery query = new GetOrderByIdQuery(id);
        Order order = await _mediator.Send(query, cancellationToken);

        return Ok(order);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Order>> GetUserOrders(CancellationToken cancellationToken)
    {
        long userId = long.Parse(User.Claims.Single(x => x.Type == CustomClaimName.Id).Value);
        GetOrdersByUserIdQuery query = new GetOrdersByUserIdQuery(userId);
        IEnumerable<Order> orders = await _mediator.Send(query, cancellationToken);

        return View("Orders", orders);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] CreateOrderCommand createCommand, CancellationToken cancellationToken)
    {
        long userId = long.Parse(User.Claims.Single(x => x.Type == CustomClaimName.Id).Value);
        createCommand.UserId = userId;
        Order createdOrder = await _mediator.Send(createCommand, cancellationToken);

        return Ok(createdOrder.Id);
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = PolicyName.Administrator)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        CancelOrderCommand command = new CancelOrderCommand(id);
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}