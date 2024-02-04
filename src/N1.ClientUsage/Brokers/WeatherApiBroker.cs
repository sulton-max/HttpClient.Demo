using System.Web;
using Microsoft.Extensions.Options;
using N1.ClientUsage.Models;
using N1.ClientUsage.Settings;

namespace N1.ClientUsage.Brokers;

public class WeatherApiBroker(HttpClient httpClient, IOptions<WeatherApiSettings> weatherApiSettings) : IWeatherApiBroker
{
    private readonly WeatherApiSettings _weatherApiSettings = weatherApiSettings.Value;

    public async ValueTask<WeatherDetails> GetWeatherDetailsAsync(string city, bool includeAirQuality = false)
    {
        // Create request url
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters["q"] = city;
        queryParameters["aqi"] = includeAirQuality ? "yes" : "no";
        queryParameters["key"] = _weatherApiSettings.ApiKey;
        
        var url = $"{_weatherApiSettings.CurrentWeatherUrl}?{queryParameters}";

        // Send request
        return await httpClient.GetFromJsonAsync<WeatherDetails>(url) ?? throw new Exception("Weather details not found");
    }
}