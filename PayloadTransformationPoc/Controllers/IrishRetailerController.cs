using Microsoft.AspNetCore.Mvc;
using PayloadTransformationPoc.Model;

namespace PayloadTransformationPoc.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class IrishRetailerController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAddress()
        {
            return Ok(PopulateIrishAddress());
        }

        [HttpPost]
        public IActionResult PostAddress([FromBody] CustomerAddress customerAddress)
        {
            return Ok();
        }

        private CustomerAddress PopulateIrishAddress()
        {
            return new CustomerAddress()
            {
                Country = "Ireland",
                Address1 = "22 Kenure Gate",
                Address2 = "Rush",
                City = "Dublin",
                ZipCode = "K56 DP82",
                IsPoBox = 1
            };
        }
    }
}
