﻿using ApplicationCore.Entities;
using DomainServices.Features.Customers.Queries;
using DomainServices.Features.Templates.Commands.Create;
using DomainServices.Features.Templates.Commands.Delete;
using DomainServices.Features.Templates.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<CustomerInfo>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllCustomersQuery()));
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(CustomerInfo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<CustomerInfo>> GetById([FromRoute] long id)
    {
        return Ok(await _mediator.Send(new GetCustomerByIdQuery(id)));
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] CustomerInfo customerInfo)
    {
        customerInfo.Id = 0;
        await _mediator.Send(new CreateCommand<CustomerInfo>(customerInfo));
        return Ok();
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] CustomerInfo customerInfo)
    {
        await _mediator.Send(new UpdateCommand<CustomerInfo>(customerInfo));
        return Ok();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id)
    {
        await _mediator.Send(new DeleteCommand<CustomerInfo>(id));
        return Ok();
    }
}