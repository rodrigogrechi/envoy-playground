using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCBPIDiscovery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscoveryBController : ControllerBase
    {
        private readonly ILogger<DiscoveryBController> _logger;

        public DiscoveryBController(ILogger<DiscoveryBController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("begin discovery");
            Console.WriteLine("Service Online");
            _logger.LogInformation("end discovery");
            return "Service Online";
        }
    }
}
