using N1.ClientUsage.Models;

namespace N1.ClientUsage.Brokers;

public interface IWeatherApiBroker
{
    ValueTask<WeatherDetails> GetWeatherDetailsAsync(string city, bool includeAirQuality = false);
}