using ApplicationCore.Entities;

namespace ApplicationCore.EqualityComparers;
public class CategoryEqualityComparerById : IEqualityComparer<Category>
{
    public bool Equals(Category? x, Category? y)
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

    public int GetHashCode(Category obj)
    {
        return obj.Id.GetHashCode();
    }
}
