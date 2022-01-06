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
    [Route("discovery/[controller]")]
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

            Uri baseUrlB = new Uri("http://discovery-b:8000/discovery/DiscoveryB");
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
                status = "Service Offline";
            }

            Console.WriteLine(status);

            _logger.LogInformation("end discovery");
            return status;
        }
    }
}
