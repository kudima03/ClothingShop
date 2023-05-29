using DomainServices.Features.ImagesInfos.Queries;
using FluentValidation;

namespace DomainServices.Features.ImagesInfos.Validators;

public class GetImageInfoByIdQueryValidator : AbstractValidator<GetImageInfoByIdQuery>
{
    public GetImageInfoByIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.Id)} must be greater than 0.");
    }
}