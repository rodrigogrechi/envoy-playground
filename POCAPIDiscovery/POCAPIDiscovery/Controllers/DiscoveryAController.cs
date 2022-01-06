using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCAPIDiscovery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscoveryAController : ControllerBase
    {
        private readonly ILogger<DiscoveryAController> _logger;

        public DiscoveryAController(ILogger<DiscoveryAController> logger)
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

            Uri baseUrlC = new Uri("http://localhost:5001/DiscoveryC");
            IRestClient clientC = new RestClient(baseUrlC);
            IRestRequest requestC = new RestRequest(Method.GET);
            IRestResponse<string> responseC = clientC.Execute<string>(requestC);

            string status;

            if (responseB.IsSuccessful && responseC.IsSuccessful)
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
