using System;
using System.Collections.Generic;

namespace CarDealershipSystem.Database.AppDbContextModels;

public partial class Feature
{
    public int Featureid { get; set; }

    public string Featurename { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
