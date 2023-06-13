using ApplicationCore.Entities;

namespace ApplicationCore.EqualityComparers;
public class ProductOptionEqualityComparerById : IEqualityComparer<ProductOption>
{
    public bool Equals(ProductOption? x, ProductOption? y)
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

        return x.Id == y.Id;
    }

    public int GetHashCode(ProductOption obj)
    {
        return obj.Id.GetHashCode();
    }
}
