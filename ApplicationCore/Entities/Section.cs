namespace ApplicationCore.Entities;

public class Section
{
    public long Id { get; set; }
    public string Name { get; set; }
    public virtual List<Category> Categories { get; } = new();
}