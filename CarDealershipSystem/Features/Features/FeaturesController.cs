using CarDealershipSystem.Api.Features;
using CarDealershipSystem.Domain.Features.Categories;
using CarDealershipSystem.Domain.Features.Categories.CategoriesModels;
using CarDealershipSystem.Domain.Features.Features.FeaturesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipSystem.Domain.Features.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : BaseController
    {
        private readonly FeaturesService _featuresService;

        public FeaturesController(FeaturesService featuresService)
        {
            _featuresService = featuresService;
        }

        [HttpGet]
        public IActionResult GetFeaturesList([FromQuery] FeaturesListRequestModel request)
        {
            var result = _featuresService.GetFeaturesList(request);
            return Execute(result);
        }
    }
}
