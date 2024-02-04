using Microsoft.AspNetCore.Mvc;
using N1.ClientUsage.Brokers;

namespace N1.ClientUsage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController(IWeatherApiBroker weatherApiBroker) : ControllerBase
{
    [HttpGet("{city}")]
    public async ValueTask<IActionResult> GetWeatherDetailsAsync([FromRoute] string city, [FromQuery] bool includeAirQuality)
    {
        var result = await weatherApiBroker.GetWeatherDetailsAsync(city, includeAirQuality);
        return Ok(result);
    }
}