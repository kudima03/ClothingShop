using ApplicationCore.Entities;
using DomainServices.Features.Sections.Commands.Create;
using DomainServices.Features.Sections.Commands.Delete;
using DomainServices.Features.Sections.Commands.Update;
using DomainServices.Features.Sections.Queries.GetAll;
using DomainServices.Features.Sections.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.Customer)]
public class SectionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Section>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Section>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllSectionsQuery query = new GetAllSectionsQuery();
        IEnumerable<Section> sections = await _mediator.Send(query, cancellationToken);

        return Ok(sections);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Section), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Section>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetSectionByIdQuery query = new GetSectionByIdQuery(id);
        Section section = await _mediator.Send(query, cancellationToken);

        return Ok(section);
    }

    [HttpPost]
    [Authorize(Policy = PolicyName.Administrator)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] CreateSectionCommand createCommand, CancellationToken cancellationToken)
    {
        Section createdSection = await _mediator.Send(createCommand, cancellationToken);

        return Ok(createdSection.Id);
    }

    [HttpPut]
    [Authorize(Policy = PolicyName.Administrator)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] UpdateSectionCommand updateCommand, CancellationToken cancellationToken)
    {
        await _mediator.Send(updateCommand, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = PolicyName.Administrator)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        DeleteSectionCommand command = new DeleteSectionCommand(id);
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}