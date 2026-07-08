using System;
using System.Collections.Generic;

namespace CarDealershipSystem.Database.AppDbContextModels;

public partial class CarDetail
{
    public int Carid { get; set; }

    public string Vinnumber { get; set; } = null!;

    public string? Enginetype { get; set; }

    public string? Transmission { get; set; }

    public string? Fueltype { get; set; }

    public int Mileage { get; set; }

    public string? Interiorcolor { get; set; }

    public string? Exteriorcolor { get; set; }

    public string? Description { get; set; }

    public int? Numberofowners { get; set; }

    public virtual Car Car { get; set; } = null!;
}
