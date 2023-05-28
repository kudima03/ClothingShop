using MediatR;

namespace DomainServices.Features.Categories.Commands.AssociateSubcategory;

public class AssociateSubcategoryWithCategoryCommand : IRequest<Unit>
{
    public AssociateSubcategoryWithCategoryCommand(long categoryId, long subcategoryId)
    {
        CategoryId = categoryId;
        SubcategoryId = subcategoryId;
    }

    public long CategoryId { get; init; }
    public long SubcategoryId { get; init; }
}