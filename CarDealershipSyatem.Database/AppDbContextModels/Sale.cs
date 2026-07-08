using System;
using System.Collections.Generic;

namespace CarDealershipSystem.Database.AppDbContextModels;

public partial class Sale
{
    public int Saleid { get; set; }

    public string Salecode { get; set; } = null!;

    public int Carid { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerContact { get; set; } = null!;

    public decimal Saleprice { get; set; }

    public DateTime Saledatetime { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
