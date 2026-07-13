using Microsoft.AspNetCore.Components;
using CarDealershipSystem.Domain;
using CarDealershipSystem.App.Services;
using CarDealershipSystem.Domain.Features.Features.FeaturesModels;

namespace CarDealershipSystem.App.Components.Features.Features;

public partial class FeaturesList : ComponentBase
{

    private FeaturesListRequestModel requestModel = new();
    private Result<FeaturesListResponseModel>? responseModel;
    private int rowNo = 0;

    protected override async Task OnInitializedAsync()
    {
        requestModel = new FeaturesListRequestModel
        {
            PageNumber = 1,
            PageSize = 10
        };

        try
        {
            responseModel = await ApiService.GetFeatures(requestModel);
        }
        catch (Exception ex)
        {
            responseModel = new Result<FeaturesListResponseModel>
            {
                IsSuccess = false,
                Message = $"Failed to communicate with API server: {ex.Message}"
            };
        }
    }
}