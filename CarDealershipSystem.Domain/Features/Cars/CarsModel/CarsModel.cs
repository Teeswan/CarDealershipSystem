using System;
using System.Collections.Generic;

namespace CarDealershipSystem.Domain.Features.Cars.CarsModel;

public class CarsListRequestModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }  
    public int? Categoryid { get; set; }   
    public int? Carbrandid { get; set; }  
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Status { get; set; }  
    public string? SortBy { get; set; }      
    public bool IsDescending { get; set; } = false;
}

public class CarsListResponseModel
{
    public List<CarsDataModel> Cars { get; set; } = new List<CarsDataModel>();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}

public class CarsDataModel
{
    public int Carid { get; set; }
    public int Categoryid { get; set; }
    public int Carbrandid { get; set; }
    public string Modelnumber { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string Vinnumber { get; set; } = string.Empty;
    public string? Enginetype { get; set; }
    public string? Transmission { get; set; }
    public string? Fueltype { get; set; }
    public int Mileage { get; set; }
    public string? Interiorcolor { get; set; }
    public string? Exteriorcolor { get; set; }
    public string? Description { get; set; }
    public int Numberofowners { get; set; }
    public string? PrimaryImageUrl { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();
    public List<string> FeatureNames { get; set; } = new List<string>();
}
public class CreateCarRequestModel
{
    public int Categoryid { get; set; }
    public int Carbrandid { get; set; }
    public string Modelnumber { get; set; } = null!;
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = "Available";
    public string Vinnumber { get; set; } = null!;
    public string? Enginetype { get; set; }
    public string? Transmission { get; set; }
    public string? Fueltype { get; set; }
    public int Mileage { get; set; }
    public string? Interiorcolor { get; set; }
    public string? Exteriorcolor { get; set; }
    public string? Description { get; set; }
    public int Numberofowners { get; set; }
    //public List<string> ImageUrls { get; set; } = new List<string>();
    //public string? PrimaryImageUrl { get; set; }
    public List<int> FeatureIds { get; set; } = new List<int>();
}

public class UpdateCarRequestModel : CreateCarRequestModel
{
    public int Carid { get; set; }
}