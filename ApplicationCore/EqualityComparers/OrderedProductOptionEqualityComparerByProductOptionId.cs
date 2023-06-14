using ApplicationCore.Entities;

namespace ApplicationCore.EqualityComparers;
public class OrderedProductOptionEqualityComparerByProductOptionId : IEqualityComparer<OrderItem>
{
    public bool Equals(OrderItem? x, OrderItem? y)
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

    public int GetHashCode(OrderItem obj)
    {
        return obj.ProductOptionId.GetHashCode();
    }
}
