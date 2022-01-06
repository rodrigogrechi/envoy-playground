using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCCPIDiscovery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscoveryCController : ControllerBase
    {
        private readonly ILogger<DiscoveryCController> _logger;

        public DiscoveryCController(ILogger<DiscoveryCController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("begin discovery");

            Uri baseUrlB = new Uri("http://localhost:5001/DiscoveryB");
            IRestClient clientB = new RestClient(baseUrlB);
            IRestRequest requestB = new RestRequest(Method.GET);
            IRestResponse<string> responseB = clientB.Execute<string>(requestB);

            string status;

            if (responseB.IsSuccessful)
            {
               status = "Service Online";
            }
            else
            {
                status = responseB.ErrorMessage;
            }

            Console.WriteLine(status);

            _logger.LogInformation("end discovery");
            return status;
        }
    }
}
