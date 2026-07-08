using CarDealershipSystem.Database.AppDbContextModels;
using CarDealershipSystem.Domain.Features.Categories.CategoriesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipSystem.Domain.Features.Categories;

public class CategoriesService
{
    private readonly AppDbContext _appDbContext;

    public CategoriesService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Result<CategoriesListResponseModel> GetCategoriesList(CategoriesListRequestModel request)
    {
        try
        {
            var categories = _appDbContext.Categories
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            Result<CategoriesListResponseModel> result = new Result<CategoriesListResponseModel>
            {
                IsSuccess = true,
                Message = "Categories retrieved successfully.",
                Data = new CategoriesListResponseModel
                {
                    Categories = categories.Select(c => new CategoriesModel
                    {
                        CategoryId = c.Categoryid,
                        CategoryName = c.Categoryname
                    }).ToList()
                }
            };

            return result;
        }
        catch (Exception ex)
        {
            Result<CategoriesListResponseModel> result = new Result<CategoriesListResponseModel>
            {
                IsSuccess = false,
                Message = ex.Message,
            };

            return result;
        }
    }
}


