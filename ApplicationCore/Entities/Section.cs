﻿using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Section : IStorable
{
    public string Name { get; set; }
    public virtual List<Category> Categories { get; } = new();
    public long Id { get; set; }
}