using CarDealershipSystem.Api.Features;
using CarDealershipSystem.Domain.Features.CarBrands.CarBrandsModels;
using CarDealershipSystem.Domain.Features.Categories;
using CarDealershipSystem.Domain.Features.Categories.CategoriesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipSystem.Domain.Features.CarBrands
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandsController : BaseController
    {
        private readonly CarBrandsService _carBrandsService;

        public CarBrandsController(CarBrandsService carBrandsService)
        {
            _carBrandsService = carBrandsService;
        }

        [HttpGet]
        public IActionResult GetCarBrandsList([FromQuery] CarBrandsListRequestModel request)
        {
            var result = _carBrandsService.GetCarBrandsList(request);
            return Execute(result);
        }
    }
}
