﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace POCAPIDiscovery.Controllers
{
    [ApiController]
    [Route("discovery/[controller]")]
    public class DiscoveryAController : ControllerBase
    {
        private readonly ILogger<DiscoveryAController> _logger;

        public DiscoveryAController(ILogger<DiscoveryAController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            _logger.LogInformation("begin discovery");

            string responseB = await this.GetRest("http://discovery-b:9000/discovery/DiscoveryB");
            string responseC = await this.GetRest("http://discovery-c:9000/discovery/DiscoveryC");

            string status;

            if (responseB != null && responseC != null)
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
