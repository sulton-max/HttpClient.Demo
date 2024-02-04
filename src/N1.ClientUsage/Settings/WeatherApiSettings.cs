namespace N1.ClientUsage.Settings;

public class WeatherApiSettings
{
    public string BaseAddress { get; set; } = default!;

    public string CurrentWeatherUrl { get; set; } = default!;
    
    public string ApiKey { get; set; } = default!;
}