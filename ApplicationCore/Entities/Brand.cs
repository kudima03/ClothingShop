﻿using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Brand : IStorable
{
    public string Name { get; set; }
    public virtual List<Product> Products { get; init; } = new();
    public long Id { get; set; }
}