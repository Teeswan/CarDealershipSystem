using CarDealershipSystem.Database.AppDbContextModels;
using CarDealershipSystem.Domain.Features.Categories.CategoriesModels;
using Microsoft.EntityFrameworkCore;
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
            int pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var query = _appDbContext.Categories
                        .AsNoTracking()
                        .Where(c => c.Categoryid > 0);

            var categories = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new Result<CategoriesListResponseModel>
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
        }
        catch (Exception ex)
        {
            return new Result<CategoriesListResponseModel>
            {
                IsSuccess = false,
                Message = ex.Message,
            };
        }
    }
}


