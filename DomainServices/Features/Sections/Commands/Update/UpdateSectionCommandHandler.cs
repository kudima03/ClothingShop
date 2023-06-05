using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
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
        Section? section = await _sectionRepository.GetFirstOrDefaultAsync(x => x.Id == request.Section.Id,
            sections => sections.Include(section1 => section1.Categories),
            cancellationToken);

        if (section is null)
        {
            throw new EntityNotFoundException($"{nameof(Section)} with id:{request.Section.Id} doesn't exist.");
        }

        section.Name = request.Section.Name;
        section.Categories.Clear();
        section.Categories.AddRange(request.Section.Categories);

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
}