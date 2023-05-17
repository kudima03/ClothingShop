﻿#nullable enable
namespace ApplicationCore.Entities;

public class CustomerInfo
{
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}