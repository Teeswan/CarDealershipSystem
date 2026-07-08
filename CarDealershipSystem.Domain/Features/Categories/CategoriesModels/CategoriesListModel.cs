namespace CarDealershipSystem.Domain.Features.Categories.CategoriesModels;

public class CategoriesListRequestModel
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}

public class CategoriesListResponseModel
{
    public List<CategoriesModel> Categories { get; set; } = new List<CategoriesModel>();

}

public class CategoriesModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;

}
