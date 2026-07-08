using CarDealershipSystem.Database.AppDbContextModels;
using CarDealershipSystem.Api.Features;
using CarDealershipSystem.Domain.Features.Categories.CategoriesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipSystem.Domain.Features.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesController(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public IActionResult GetCategoriesList([FromQuery] CategoriesListRequestModel request)
        {
            var result = _categoriesService.GetCategoriesList(request);
            return Execute(result);
        }
    }
}
