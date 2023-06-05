using ApplicationCore.Entities;
using DomainServices.Features.Customers.Commands.Create;
using DomainServices.Features.Customers.Commands.Delete;
using DomainServices.Features.Customers.Commands.Update;
using DomainServices.Features.Customers.Queries.GetAll;
using DomainServices.Features.Customers.Queries.GetById;
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
        IEnumerable<CustomerInfo> customers = await _mediator.Send(new GetAllCustomersQuery());
        return Ok(customers);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(CustomerInfo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<CustomerInfo>> GetById([FromRoute] long id)
    {
        CustomerInfo customer = await _mediator.Send(new GetCustomerByIdQuery(id));
        return Ok(customer);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] CustomerInfo customerInfo)
    {
        CustomerInfo createdCustomer = await _mediator.Send(new CreateCustomerCommand(customerInfo));
        return Ok(createdCustomer.Id);
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] CustomerInfo customerInfo)
    {
        await _mediator.Send(new UpdateCustomerCommand(customerInfo));
        return Ok();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id)
    {
        await _mediator.Send(new DeleteCustomerCommand(id));
        return Ok();
    }
}