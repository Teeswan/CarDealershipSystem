using System;
using System.Collections.Generic;

namespace CarDealershipSystem.Database.AppDbContextModels;

public partial class Appointment
{
    public int Appointmentid { get; set; }

    public string Appointmentcode { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string CustomerContact { get; set; } = null!;

    public int Carid { get; set; }

    public DateTime Appointmentdatetime { get; set; }

    public string Appointmenttype { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Car Car { get; set; } = null!;
}
