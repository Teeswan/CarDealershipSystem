using CarDealershipSystem.Database.AppDbContextModels;
using CarDealershipSystem.Domain;
using CarDealershipSystem.Domain.Features.Cars.CarsModels;
using CarDealershipSystem.Domain.Features.Categories.CategoriesModels;
using CarDealershipSystem.Domain.Features.Features.FeaturesModels;

namespace CarDealershipSystem.App.Services;

public class ApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configration;
    private readonly string _baseUrl;

    public ApiService(IHttpClientFactory httpClientFactory, IConfiguration configration)
    {
        _httpClientFactory = httpClientFactory;
        _configration = configration;
        _baseUrl = _configration.GetValue<string>("BackendApiUrl")!;
    }
    public async Task<Result<CategoriesListResponseModel>> GetCategories(CategoriesListRequestModel requestModel)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(_baseUrl);
        string url = $"{ApiEndpoints.Categories}?pageNo={requestModel.PageNumber}&pageSize={requestModel.PageSize}";

        var response = await httpClient.GetAsync(url);

        var result = await response.Content.ReadFromJsonAsync<Result<CategoriesListResponseModel>>();

        return result!;
    }

    public async Task<Result<CarsListResponseModel>> GetCars(CarsListRequestModel requestModel)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(_baseUrl);
        string url = $"{ApiEndpoints.Cars}?pageNo={requestModel.PageNumber}&pageSize={requestModel.PageSize}";

        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<Result<CarsListResponseModel>>();
        return result!;
    }

    public async Task<Result<FeaturesListResponseModel>> GetFeatures(FeaturesListRequestModel requestModel)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(_baseUrl);
        string url = $"{ApiEndpoints.Features}?pageNo={requestModel.PageNumber}&pageSize={requestModel.PageSize}";

        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<Result<FeaturesListResponseModel>>();
        return result!;
    }

    public class ApiEndpoints
    {
        #region Categories
        public const string Categories = "api/categories";
        public const string CreateCategory = "api/categories/create";
        public const string UpdateCategory = "api/categories/update";
        public const string GetCategoryById = "api/categories/getbyid";
        public const string DeleteCategory = "api/categories/delete";
        #endregion
        #region Cars
        public const string Cars = "api/cars";
        public const string CreateCar = "api/cars/create";
        public const string UpdateCar = "api/cars/update";
        public const string GetCarById = "api/cars/getbyid";
        public const string DeleteCar = "api/cars/delete";
        #endregion
        #region Features
        public const string Features = "api/features";
        public const string CreateFeature = "api/features/create";
        public const string UpdateFeature = "api/features/update";
        public const string GetFeatureById = "api/features/getbyid";
        public const string DeleteFeature = "api/features/delete";
        #endregion
    }

}
