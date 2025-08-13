﻿using Ordering.Core.Common;

namespace Ordering.Core.Entities;

public class Order : BaseEntity
{
    public string? UserName { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? State { get; set; }
    public PaymentMethodEnum PaymentMethod { get; set; }
}