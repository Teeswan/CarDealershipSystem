namespace CarDealershipSystem.Domain.Features.CarBrands.CarBrandsModels;

public class CarBrandsListRequestModel
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}

public class CarBrandsListResponseModel
{
    public List<CarBrandsModel> CarBrands { get; set; } = new List<CarBrandsModel>();

}

public class CarBrandsModel
{
    public int CarBrandId { get; set; }
    public string CarBrandName { get; set; } = string.Empty;

}
