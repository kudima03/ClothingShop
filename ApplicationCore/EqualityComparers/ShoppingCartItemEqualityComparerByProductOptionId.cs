using ApplicationCore.Entities;

namespace ApplicationCore.EqualityComparers;
public class ShoppingCartItemEqualityComparerByProductOptionId : IEqualityComparer<ShoppingCartItem>
{
    public bool Equals(ShoppingCartItem x, ShoppingCartItem y)
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

    public int GetHashCode(ShoppingCartItem obj)
    {
        return obj.ProductOptionId.GetHashCode();
    }
}
