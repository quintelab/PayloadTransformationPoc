using JUST;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace PayloadTransformationPoc.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProxyLayerController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;

        public ProxyLayerController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("{country}")]
        public async Task<IActionResult> Get(string country)
        {
            switch (country)
            {
                case "Brazil":
                    return await CallGetBrazilApi();
                case "Ireland":
                    return await CallGetIrelandApi();
                default:
                    return Ok();
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] JsonElement data, string country)
        {
            switch (country)
            {
                case "Brazil":
                    await CallPostBrazilApi(data);
                    break;
                case "Ireland":
                    await CallPostIrelandApi(data);
                    break;
                default:
                    break;
            }
            
            return Ok();
        }

        private async Task<IActionResult> CallGetBrazilApi()
        {
            var client = new RestClient($"http://localhost:56318/brazilianRetailer/GetAddress");
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);

            string transformedResponse = Transform("ResponseForBrazil", response.Content);

            return Ok(transformedResponse);
        }

        private async Task<IRestResponse> CallPostBrazilApi(JsonElement data)
        {
            var client = new RestClient($"http://localhost:56318/brazilianRetailer/PostAddress");
            var request = new RestRequest(Method.POST);            

            string transformedRequest = Transform("RequestForBrazil", data.GetRawText());
            request.AddJsonBody(transformedRequest);

            return await client.ExecuteAsync(request);
        }

        private async Task<IActionResult> CallGetIrelandApi()
        {
            var client = new RestClient($"http://localhost:56318/irishRetailer/GetAddress");
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);

            string transformedResponse = Transform("ResponseForIreland", response.Content);

            return Ok(transformedResponse);
        }

        private async Task<IRestResponse> CallPostIrelandApi(JsonElement data)
        {
            var client = new RestClient($"http://localhost:56318/irishRetailer/PostAddress");
            var request = new RestRequest(Method.POST);

            string transformedRequest = Transform("RequestForIreland", data.GetRawText());
            request.AddJsonBody(transformedRequest);

            return await client.ExecuteAsync(request);
        }

        private string Transform(string transformerName, string originalPayload)
        {
            //read the transformer from a JSON file
            string transformer = System.IO.File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, $"Transformers/{transformerName}.json"));

            // do the actual transformation
            return new JsonTransformer().Transform(transformer, originalPayload);
        }
    }
}
