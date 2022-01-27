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
        private readonly IHttpClientFactory _clientFactory;

        public DiscoveryCController(ILogger<DiscoveryCController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            _logger.LogInformation("begin discovery");

            string responseB = await this.GetRest("http://localhost:9000/discovery/DiscoveryB");

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
                using (HttpClient httpClient = _clientFactory.CreateClient("externalapi-client"))
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
