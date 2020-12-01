using Microsoft.AspNetCore.Mvc;
using PayloadTransformationPoc.Model;

namespace PayloadTransformationPoc.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BrazilianRetailerController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAddress()
        {
            return Ok(PopulateBrazilianAddress());
        }

        [HttpPost]
        public IActionResult PostAddress([FromBody] CustomerAddress customerAddress)
        {
            return Ok();
        }

        private CustomerAddress PopulateBrazilianAddress()
        {
            return new CustomerAddress()
            {
                Country = "Brazil",
                Address1 = "Rua Visconde de Porto Seguro",
                Address2 = "Quadra 12",
                Address3 = "90",
                City = "Sao Bernardo",
                State = "Sao Paulo",
                ZipCode = "04642-000",
                IsPoBox = 1
            };
        }
    }
}
