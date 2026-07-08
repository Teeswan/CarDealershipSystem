using CarDealershipSystem.Database.AppDbContextModels;
using CarDealershipSystem.Domain.Features.Categories.CategoriesModels;
using CarDealershipSystem.Domain.Features.Features.FeaturesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipSystem.Domain.Features.Features
{
    public class FeaturesService
    {
        private readonly AppDbContext _appDbContext;

        public FeaturesService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Result<FeaturesListResponseModel> GetFeaturesList(FeaturesListRequestModel request)
        {
            try
            {
                var features = _appDbContext.Features
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                Result<FeaturesListResponseModel> result = new Result<FeaturesListResponseModel>
                {
                    IsSuccess = true,
                    Message = "Features retrieved successfully.",
                    Data = new FeaturesListResponseModel
                    {
                        Features = features.Select(f => new FeaturesModel
                        {
                            FeatureId = f.Featureid,
                            FeatureName = f.Featurename
                        }).ToList()
                    }
                };

                return result;
            }
            catch (Exception ex)
            {
                Result<FeaturesListResponseModel> result = new Result<FeaturesListResponseModel>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

                return result;
            }
        }
    }

}
