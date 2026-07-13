using CarDealershipSystem.Domain;
using CarDealershipSystem.Domain.Features.Categories.CategoriesModels;

namespace CarDealershipSystem.App.Components.Features.Categories
{
    public partial class CategoriesList
    {
        private CategoriesListRequestModel requestModel = new();
        private Result<CategoriesListResponseModel> responseModel = new();
        private int rowNo = 0;

        protected override async Task OnInitializedAsync()
        {
            requestModel = new CategoriesListRequestModel
            {
                PageNumber = 1,
                PageSize = 10
            };
            responseModel = await ApiService.GetCategories(requestModel);
        }
    }
}
