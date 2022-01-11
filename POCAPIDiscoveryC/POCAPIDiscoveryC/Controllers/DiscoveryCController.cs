using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task<string> Get()
        {
            _logger.LogInformation("begin discovery");

            string responseB = await this.GetRest("http://discovery-b:9000/discovery/DiscoveryB");

            string status;

            if (responseB != null)
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

        private async Task<string> GetRest(string url)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    Uri baseUrl = new Uri(url);
                    HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(baseUrl);

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    return await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
