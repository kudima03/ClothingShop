﻿using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class Subcategory : StorableEntity
{
    public string Name { get; set; }
    public virtual List<Category> Categories { get; init; } = new();
    public virtual List<Product> Products { get; init; } = new();
    public long Id { get; set; }
}