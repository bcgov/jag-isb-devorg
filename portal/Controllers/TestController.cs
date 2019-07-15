using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using dynamics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;


        public TestController(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger(typeof(TestController));
        }

        /// <summary>
        /// Test Dynamics connection.
        /// </summary>
        /// <returns>OK if successful</returns>
        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult> Test()
        {
            _logger.LogError("Testing Dynamics");

            var client = DynamicsClient.GenerateClient(Configuration);

            string url = "https://devorg.dev.jag.gov.bc.ca/api/data/v9.0/WhoAmI";

            HttpRequestMessage _httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            var _httpResponse2 = await client.SendAsync(_httpRequest);

            HttpStatusCode _statusCode = _httpResponse2.StatusCode;

            var _responseString = _httpResponse2.ToString();

            var _responseContent2 = await _httpResponse2.Content.ReadAsStringAsync();



            return Content(_responseContent2);

        }

    }
}
