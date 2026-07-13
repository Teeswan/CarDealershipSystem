using CarDealershipSystem.Database.AppDbContextModels;
using CarDealershipSystem.Domain.Features.Categories.CategoriesModels;
using CarDealershipSystem.Domain.Features.Features.FeaturesModels;
using Microsoft.EntityFrameworkCore;
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
                int pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
                int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

                var features = _appDbContext.Features
                    .AsNoTracking() 
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
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
