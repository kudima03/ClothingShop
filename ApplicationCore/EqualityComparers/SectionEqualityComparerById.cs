using ApplicationCore.Entities;

namespace ApplicationCore.EqualityComparers;
public class SectionEqualityComparerById : IEqualityComparer<Section>
{
    public bool Equals(Section? x, Section? y)
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

    public int GetHashCode(Section obj)
    {
        return obj.Id.GetHashCode();
    }
}
