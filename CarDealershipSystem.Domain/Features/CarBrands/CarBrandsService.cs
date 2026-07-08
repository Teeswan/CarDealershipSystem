using CarDealershipSystem.Database.AppDbContextModels;
using CarDealershipSystem.Domain.Features.CarBrands.CarBrandsModels;
using CarDealershipSystem.Domain.Features.Categories.CategoriesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipSystem.Domain.Features.CarBrands
{
    public class CarBrandsService
    {
        private readonly AppDbContext _appDbContext;

        public CarBrandsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Result<CarBrandsListResponseModel> GetCarBrandsList(CarBrandsListRequestModel request)
        {
            try
            {
                var carbrands = _appDbContext.CarBrands
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                Result<CarBrandsListResponseModel> result = new Result<CarBrandsListResponseModel>
                {
                    IsSuccess = true,
                    Message = "Car brands retrieved successfully.",
                    Data = new CarBrandsListResponseModel       
                    {
                        CarBrands = carbrands.Select(c => new CarBrandsModel
                        {
                            CarBrandId = c.Carbrandid,
                            CarBrandName = c.Carbrandname
                        }).ToList()
                    }
                };

                return result;
            }
            catch (Exception ex)
            {
                Result<CarBrandsListResponseModel> result = new Result<CarBrandsListResponseModel>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

                return result;
            }
        }
    }

}
