using System;
using System.Collections.Generic;

namespace CarDealershipSystem.Database.AppDbContextModels;

public partial class CarImage
{
    public int Imageid { get; set; }

    public int Carid { get; set; }

    public string Imageurl { get; set; } = null!;

    public bool Isprimary { get; set; }

    public virtual Car Car { get; set; } = null!;
}
