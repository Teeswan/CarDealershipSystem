using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CarDealershipSystem.Database.AppDbContextModels;
using CarDealershipSystem.Domain.Features.Cars.CarsModel;

namespace CarDealershipSystem.Domain.Features.Cars
{
    public class CarsService
    {
        private readonly AppDbContext _context;

        public CarsService(AppDbContext context)
        {
            _context = context;
        }

        public Result<int> CreateCar(CreateCarRequestModel request)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (_context.CarDetails.Any(cd => cd.Vinnumber == request.Vinnumber))
                {
                    return new Result<int> { IsSuccess = false, Message = "A vehicle with this VIN number already exists." };
                }

                var car = new Car
                {
                    Categoryid = request.Categoryid,
                    Carbrandid = request.Carbrandid,
                    Modelnumber = request.Modelnumber,
                    Year = request.Year,
                    Price = request.Price,
                    Status = request.Status
                };
                _context.Cars.Add(car);
                _context.SaveChanges(); 

                var detail = new CarDetail
                {
                    Carid = car.Carid,
                    Vinnumber = request.Vinnumber,
                    Enginetype = request.Enginetype,
                    Transmission = request.Transmission,
                    Fueltype = request.Fueltype,
                    Mileage = request.Mileage,
                    Interiorcolor = request.Interiorcolor,
                    Exteriorcolor = request.Exteriorcolor,
                    Description = request.Description,
                    Numberofowners = request.Numberofowners
                };
                _context.CarDetails.Add(detail);

                //foreach (var url in request.ImageUrls)
                //{
                //    _context.CarImages.Add(new CarImage
                //    {
                //        Carid = car.Carid,
                //        Imageurl = url,
                //        Isprimary = (url == request.PrimaryImageUrl)
                //    });
                //}

                if (request.FeatureIds != null && request.FeatureIds.Any())
                {
                    var databaseFeatures = _context.Features.Where(f => request.FeatureIds.Contains(f.Featureid)).ToList();
                    foreach (var feature in databaseFeatures)
                    {
                        car.Features.Add(feature);
                    }
                }

                _context.SaveChanges();
                transaction.Commit();

                return new Result<int> { IsSuccess = true, Data = car.Carid };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new Result<int> { IsSuccess = false, Message = $"Creation failed: {ex.Message}" };
            }
        }

        public Result<CarsListResponseModel> GetCarsList(CarsListRequestModel request)
        {
            try
            {
                var query = _context.Cars
                    .Include(c => c.Carbrand)
                    .Include(c => c.Category)
                    .Include(c => c.CarImages)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                    query = query.Where(c => c.Modelnumber.Contains(request.SearchTerm));
                if (request.Categoryid.HasValue)
                    query = query.Where(c => c.Categoryid == request.Categoryid.Value);
                if (request.Carbrandid.HasValue)
                    query = query.Where(c => c.Carbrandid == request.Carbrandid.Value);
                if (request.MinPrice.HasValue)
                    query = query.Where(c => c.Price >= request.MinPrice.Value);
                if (request.MaxPrice.HasValue)
                    query = query.Where(c => c.Price <= request.MaxPrice.Value);
                if (!string.IsNullOrWhiteSpace(request.Status))
                    query = query.Where(c => c.Status == request.Status);

                query = request.SortBy?.ToLower() switch
                {
                    "price" => request.IsDescending ? query.OrderByDescending(c => c.Price) : query.OrderBy(c => c.Price),
                    "year" => request.IsDescending ? query.OrderByDescending(c => c.Year) : query.OrderBy(c => c.Year),
                    _ => query.OrderBy(c => c.Carid)
                };

                int totalCount = query.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

                var items = query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(c => new CarsDataModel
                    {
                        Carid = c.Carid,
                        Categoryid = c.Categoryid,
                        Carbrandid = c.Carbrandid,
                        Modelnumber = c.Modelnumber,
                        Year = c.Year,
                        Price = c.Price,
                        Status = c.Status,
                        BrandName = c.Carbrand.Carbrandname,
                        CategoryName = c.Category.Categoryname,
                        //PrimaryImageUrl = c.CarImages.FirstOrDefault(img => img.Isprimary).Imageurl ?? c.CarImages.FirstOrDefault().Imageurl
                    }).ToList();

                return new Result<CarsListResponseModel>
                {
                    IsSuccess = true,
                    Data = new CarsListResponseModel { Cars = items, TotalCount = totalCount, TotalPages = totalPages, CurrentPage = request.PageNumber }
                };
            }
            catch (Exception ex)
            {
                return new Result<CarsListResponseModel> { IsSuccess = false, Message = ex.Message };
            }
        }

        public Result<CarsDataModel> GetCarById(int carId)
        {
            try
            {
                var car = _context.Cars
                    .Include(c => c.Carbrand).Include(c => c.Category).Include(c => c.CarDetail)
                    .Include(c => c.CarImages).Include(c => c.Features)
                    .FirstOrDefault(c => c.Carid == carId);

                if (car == null) return new Result<CarsDataModel> { IsSuccess = false, Message = "Vehicle not found." };

                return new Result<CarsDataModel>
                {
                    IsSuccess = true,
                    Data = new CarsDataModel
                    {
                        Carid = car.Carid,
                        Categoryid = car.Categoryid,
                        Carbrandid = car.Carbrandid,
                        Modelnumber = car.Modelnumber,
                        Year = car.Year,
                        Price = car.Price,
                        Status = car.Status,
                        BrandName = car.Carbrand.Carbrandname,
                        CategoryName = car.Category.Categoryname,
                        Vinnumber = car.CarDetail?.Vinnumber ?? string.Empty,
                        Enginetype = car.CarDetail?.Enginetype,
                        Transmission = car.CarDetail?.Transmission,
                        Fueltype = car.CarDetail?.Fueltype,
                        Mileage = car.CarDetail?.Mileage ?? 0,
                        Interiorcolor = car.CarDetail?.Interiorcolor,
                        Exteriorcolor = car.CarDetail?.Exteriorcolor,
                        Description = car.CarDetail?.Description,
                        Numberofowners = car.CarDetail?.Numberofowners ?? 0,
                        //PrimaryImageUrl = car.CarImages.FirstOrDefault(img => img.Isprimary).Imageurl ?? car.CarImages.FirstOrDefault().Imageurl,
                        //ImageUrls = car.CarImages.Select(img => img.Imageurl).ToList(),
                        FeatureNames = car.Features.Select(f => f.Featurename).ToList()
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<CarsDataModel> { IsSuccess = false, Message = ex.Message };
            }
        }

        public Result<bool> UpdateCar(UpdateCarRequestModel request)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var car = _context.Cars
                    .Include(c => c.CarDetail)
                    .Include(c => c.CarImages)
                    .Include(c => c.Features)
                    .FirstOrDefault(c => c.Carid == request.Carid);

                if (car == null) return new Result<bool> { IsSuccess = false, Message = "Car record to update was not found." };

                car.Categoryid = request.Categoryid;
                car.Carbrandid = request.Carbrandid;
                car.Modelnumber = request.Modelnumber;
                car.Year = request.Year;
                car.Price = request.Price;
                car.Status = request.Status;

                if (car.CarDetail == null) car.CarDetail = new CarDetail { Carid = car.Carid };
                car.CarDetail.Vinnumber = request.Vinnumber;
                car.CarDetail.Enginetype = request.Enginetype;
                car.CarDetail.Transmission = request.Transmission;
                car.CarDetail.Fueltype = request.Fueltype;
                car.CarDetail.Mileage = request.Mileage;
                car.CarDetail.Interiorcolor = request.Interiorcolor;
                car.CarDetail.Exteriorcolor = request.Exteriorcolor;
                car.CarDetail.Description = request.Description;
                car.CarDetail.Numberofowners = request.Numberofowners;

                _context.CarImages.RemoveRange(car.CarImages);

                //foreach (var url in request.ImageUrls)
                //{
                //    _context.CarImages.Add(new CarImage
                //    {
                //        Carid = car.Carid,
                //        Imageurl = url,
                //        Isprimary = (url == request.PrimaryImageUrl)
                //    });
                //}

                car.Features.Clear();
                if (request.FeatureIds != null && request.FeatureIds.Any())
                {
                    var databaseFeatures = _context.Features.Where(f => request.FeatureIds.Contains(f.Featureid)).ToList();
                    foreach (var feature in databaseFeatures)
                    {
                        car.Features.Add(feature);
                    }
                }

                _context.SaveChanges();
                transaction.Commit();
                return new Result<bool> { IsSuccess = true, Data = true };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new Result<bool> { IsSuccess = false, Message = $"Update failed: {ex.Message}" };
            }
        }

        public Result<bool> DeleteCar(int carId)
        {
            try
            {
                var car = _context.Cars.FirstOrDefault(c => c.Carid == carId);
                if (car == null) return new Result<bool> { IsSuccess = false, Message = "Car record to remove was not found." };

                _context.Cars.Remove(car);
                _context.SaveChanges();

                return new Result<bool> { IsSuccess = true, Data = true };
            }
            catch (Exception ex)
            {
                return new Result<bool> { IsSuccess = false, Message = $"Deletion failed: {ex.Message}" };
            }
        }
    }
}