using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Sections.Commands.Update;

public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Unit>
{
    private readonly IRepository<Section> _sectionRepository;

    public UpdateSectionCommandHandler(IRepository<Section> sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<Unit> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        Section section = await ValidateAndGetSectionAsync(request.Id, cancellationToken);

        if (section.Name != request.Name)
        {
            await ValidateSectionNameAsync(request.Name, cancellationToken);
        }

        section.Name = request.Name;

        try
        {
            await _sectionRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform update {nameof(Section)} operation. Check input.");
        }

        return Unit.Value;
    }

    private async Task<Section> ValidateAndGetSectionAsync(long sectionId,
        CancellationToken cancellationToken = default)
    {
        Section? section = await _sectionRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == sectionId,
            cancellationToken: cancellationToken);

        if (section is null)
        {
            throw new EntityNotFoundException($"{nameof(Section)} with id:{sectionId} doesn't exist.");
        }

        return section;
    }

    private async Task ValidateSectionNameAsync(string name, CancellationToken cancellationToken = default)
    {
        bool nameExists = await _sectionRepository.ExistsAsync(x => x.Name == name, cancellationToken);
        if (nameExists)
        {
            throw new ValidationException(new[] { new ValidationFailure("Brand.Name", "Such brand already exists!") });
        }
    }
}