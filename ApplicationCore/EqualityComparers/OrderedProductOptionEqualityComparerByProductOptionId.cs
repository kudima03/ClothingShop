using ApplicationCore.Entities;

namespace ApplicationCore.EqualityComparers;
public class OrderedProductOptionEqualityComparerByProductOptionId : IEqualityComparer<OrderedProductOption>
{
    public bool Equals(OrderedProductOption? x, OrderedProductOption? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.ProductOptionId == y.ProductOptionId;
    }

    public int GetHashCode(OrderedProductOption obj)
    {
        return obj.ProductOptionId.GetHashCode();
    }
}
