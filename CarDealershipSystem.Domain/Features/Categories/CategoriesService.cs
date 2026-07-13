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

    #region Read (List)
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
    #endregion

    #region Create
    public Result<string> CreateCategory(CategoryCreateRequestModel request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.CategoryName))
            {
                return new Result<string>
                {
                    IsSuccess = false,
                    Message = "Category name cannot be empty."
                };
            }

            var category = new Category
            {
                Categoryname = request.CategoryName
            };

            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();

            return new Result<string>
            {
                IsSuccess = true,
                Message = "Category created successfully."
            };
        }
        catch (Exception ex)
        {
            return new Result<string>
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
    #endregion

    #region Update
    public Result<string> UpdateCategory(CategoryUpdateRequestModel request)
    {
        try
        {
            var category = _appDbContext.Categories
                .FirstOrDefault(c => c.Categoryid == request.CategoryId);

            if (category == null)
            {
                return new Result<string>
                {
                    IsSuccess = false,
                    Message = "Category not found."
                };
            }

            if (string.IsNullOrWhiteSpace(request.CategoryName))
            {
                return new Result<string>
                {
                    IsSuccess = false,
                    Message = "Category name cannot be empty."
                };
            }

            category.Categoryname = request.CategoryName;

            _appDbContext.Categories.Update(category);
            _appDbContext.SaveChanges();

            return new Result<string>
            {
                IsSuccess = true,
                Message = "Category updated successfully."
            };
        }
        catch (Exception ex)
        {
            return new Result<string>
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
    #endregion

    #region Delete
    public Result<string> DeleteCategory(int categoryId)
    {
        try
        {
            var category = _appDbContext.Categories
                .FirstOrDefault(c => c.Categoryid == categoryId);

            if (category == null)
            {
                return new Result<string>
                {
                    IsSuccess = false,
                    Message = "Category not found."
                };
            }

            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();

            return new Result<string>
            {
                IsSuccess = true,
                Message = "Category deleted successfully."
            };
        }
        catch (Exception ex)
        {
            return new Result<string>
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
    #endregion
}