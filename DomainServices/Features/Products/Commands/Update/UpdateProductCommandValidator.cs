using FluentValidation;

namespace DomainServices.Features.Products.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Name)} cannot be null or empty");

        RuleForEach(x => x.ProductOptionsDto)
            .NotNull()
            .ChildRules(x =>
            {
                x.RuleFor(c => c.Quantity)
                    .InclusiveBetween(1, int.MaxValue);
                x.RuleFor(c => c.Price)
                    .InclusiveBetween(1, decimal.MaxValue);
                x.RuleFor(c => c.Size)
                    .NotNull()
                    .NotEmpty();
                x.RuleFor(c => c.ProductColor)
                    .NotNull()
                    .ChildRules(v =>
                    {
                        v.RuleFor(b => b.ColorHex)
                            .NotNull()
                            .NotEmpty();

                        v.RuleForEach(n => n.ImagesInfos)
                            .NotNull()
                            .ChildRules(a =>
                            {
                                a.RuleFor(s => s.Url)
                                    .NotNull()
                                    .NotEmpty();
                            });
                    });
            });
    }
}