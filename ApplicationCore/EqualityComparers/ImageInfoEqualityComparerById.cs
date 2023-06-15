using ApplicationCore.Entities;

namespace ApplicationCore.EqualityComparers;
public class ImageInfoEqualityComparerById : IEqualityComparer<ImageInfo>
{
    public bool Equals(ImageInfo? x, ImageInfo? y)
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

    public int GetHashCode(ImageInfo obj)
    {
        return obj.Id.GetHashCode();
    }
}
