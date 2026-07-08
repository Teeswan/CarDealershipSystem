using System;
using System.Collections.Generic;

namespace CarDealershipSystem.Database.AppDbContextModels;

public partial class Payment
{
    public int Paymentid { get; set; }

    public int Saleid { get; set; }

    public string Paymentmethod { get; set; } = null!;

    public string Paymentstatus { get; set; } = null!;

    public decimal Amountpaid { get; set; }

    public DateTime Paymentdatetime { get; set; }

    public virtual Sale Sale { get; set; } = null!;
}
