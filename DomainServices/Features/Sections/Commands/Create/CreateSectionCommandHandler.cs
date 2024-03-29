﻿using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Sections.Commands.Create;

public class CreateSectionCommandHandler(IRepository<Section> sectionsRepository) : IRequestHandler<CreateSectionCommand, Section>
{
    public async Task<Section> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        await ValidateSectionNameAsync(request.Name, cancellationToken);

        Section newSection = new Section
        {
            Name = request.Name
        };

        try
        {
            Section? section = await sectionsRepository.InsertAsync(newSection, cancellationToken);
            await sectionsRepository.SaveChangesAsync(cancellationToken);

            return section;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Section)} operation. Check input.");
        }
    }

    private async Task ValidateSectionNameAsync(string name, CancellationToken cancellationToken = default)
    {
        bool nameExists = await sectionsRepository.ExistsAsync(x => x.Name == name, cancellationToken);

        if (nameExists)
        {
            throw new ValidationException
                (new[]
                {
                    new ValidationFailure("Section.Name", "Such section name already exists!")
                });
        }
    }
}