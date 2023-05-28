using MediatR;

namespace DomainServices.Features.Categories.Commands.DeassociateSubcategory;

public class DeassociateSubcategoryWithCategoryCommand : IRequest<Unit>
{
    public DeassociateSubcategoryWithCategoryCommand(long categoryId, long subcategoryId)
    {
        CategoryId = categoryId;
        SubcategoryId = subcategoryId;
    }

    public long CategoryId { get; init; }
    public long SubcategoryId { get; init; }
}