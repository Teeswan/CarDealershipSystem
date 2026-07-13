using CarDealershipSystem.App.Services;
using CarDealershipSystem.Domain;
using CarDealershipSystem.Domain.Features.Cars.CarsModel;
using Microsoft.AspNetCore.Components;

namespace CarDealershipSystem.App.Components.Features.Cars;

public partial class CarsModelList : ComponentBase
{
    private CarsListRequestModel requestModel = new();
    private Result<CarsListResponseModel>? responseModel;
    private int rowNo = 0;

    protected override async Task OnInitializedAsync()
    {
        requestModel = new CarsListRequestModel
        {
            PageNumber = 1,
            PageSize = 10
        };

        try
        {
            responseModel = await ApiService.GetCars(requestModel);
        }
        catch (Exception ex)
        {
            responseModel = new Result<CarsListResponseModel>
            {
                IsSuccess = false,
                Message = $"Failed to communicate with API server: {ex.Message}"
            };
        }
    }
}