using System;
using System.Collections.Generic;

namespace CarDealershipSystem.Database.AppDbContextModels;

public partial class Category
{
    public int Categoryid { get; set; }

    public string Categoryname { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
