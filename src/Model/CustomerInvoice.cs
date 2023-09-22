using System;
using System.Collections.Generic;

namespace Chinook.Src.Model;

public partial class CustomerInvoice
{
    public int CustomerId { get; set; }

    public string? FirstName { get; set; }

    public decimal? Total { get; set; }
}
