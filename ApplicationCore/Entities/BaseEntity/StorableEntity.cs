namespace ApplicationCore.Entities.BaseEntity;

public abstract class StorableEntity
{
    public long Id { get; set; }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}