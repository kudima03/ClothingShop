using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Sections.Commands.Create;

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Section>
{
    private readonly IRepository<Section> _repository;

    public CreateSectionCommandHandler(IRepository<Section> repository)
    {
        _repository = repository;
    }

    public async Task<Section> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Section? section = await _repository.InsertAsync(request.Section, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return section;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Section)} operation. Check input.");
        }
    }
}