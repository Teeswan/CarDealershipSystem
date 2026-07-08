using Microsoft.AspNetCore.Mvc;
using CarDealershipSystem.Domain.Features.Cars;
using CarDealershipSystem.Domain.Features.Cars.CarsModel;

namespace CarDealershipSystem.Api.Features.Cars
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : BaseController 
    {
        private readonly CarsService _carsService;

        public CarsController(CarsService carsService)
        {
            _carsService = carsService;
        }

        [HttpGet]
        public IActionResult GetCarsList([FromQuery] CarsListRequestModel request)
        {
            var result = _carsService.GetCarsList(request);
            return Execute(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            var result = _carsService.GetCarById(id);
            return Execute(result);
        }

        [HttpPost]
        public IActionResult CreateCar([FromBody] CreateCarRequestModel request)
        {
            var result = _carsService.CreateCar(request);
            return Execute(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCar(int id, [FromBody] UpdateCarRequestModel request)
        {
            request.Carid = id;

            var result = _carsService.UpdateCar(request);
            return Execute(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var result = _carsService.DeleteCar(id);
            return Execute(result);
        }
    }
}