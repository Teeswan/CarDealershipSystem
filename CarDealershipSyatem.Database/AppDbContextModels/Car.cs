using System;
using System.Collections.Generic;

namespace CarDealershipSystem.Database.AppDbContextModels;

public partial class Car
{
    public int Carid { get; set; }

    public int Categoryid { get; set; }

    public int Carbrandid { get; set; }

    public string Modelnumber { get; set; } = null!;

    public int Year { get; set; }

    public decimal Price { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual CarDetail? CarDetail { get; set; }
    
    public virtual ICollection<CarImage> CarImages { get; set; } = new List<CarImage>();

    public virtual CarBrand Carbrand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual Sale? Sale { get; set; }

    public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();
}
