using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Steeltoe.Common.Discovery;
using System.Net.Http;
using Steeltoe.Discovery;

[Route("api/[controller]")]
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger _logger;
    DiscoveryHttpClientHandler _handler;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IDiscoveryClient client)
    {
        _logger = logger;
        _handler = new DiscoveryHttpClientHandler(client);
    }

    // GET api/values
    [HttpGet]
    public async Task<string> Get()
    {
        var client = new HttpClient(_handler, false);
        return await client.GetStringAsync("http://EurekaRegisterExample/api/WeatherForecast");
    }
}