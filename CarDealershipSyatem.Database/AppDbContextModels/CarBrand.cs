using System;
using System.Collections.Generic;

namespace CarDealershipSystem.Database.AppDbContextModels;

public partial class CarBrand
{
    public int Carbrandid { get; set; }

    public string Carbrandname { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
