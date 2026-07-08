namespace CarDealershipSystem.Domain.Features.Features.FeaturesModels;

public class FeaturesListRequestModel
{
    public int PageNumber { get; set; } 
    public int PageSize { get; set; } 

}

public class FeaturesListResponseModel
{
    public List<FeaturesModel> Features { get; set; } = new List<FeaturesModel>();

}

public class FeaturesModel
{
    public int FeatureId { get; set; }
    public string FeatureName { get; set; } = string.Empty;

}
