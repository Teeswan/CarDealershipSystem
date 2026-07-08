using CarDealershipSystem.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarDealershipSystem.Api.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult Execute(object data)
        {
            string json = JsonConvert.SerializeObject(data); //object to JSON string
            Result<object> result = System.Text.Json.JsonSerializer.Deserialize<Result<object>>(json)!;
            if (result.IsSuccess)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest(data);
            } 
        }
    }
}
