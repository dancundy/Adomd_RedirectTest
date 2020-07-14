using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdomdController : ControllerBase
    {
        // GET: api/Adomd
        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            ConnectionManager connectionManager = new ConnectionManager(configuration);

            string customData = "12";
            string role = "default";
            var conn =  await connectionManager.GetOpenAasConnectionAsync();
        
            return new string[] { "Result", "Success" };
        }
    }
}
