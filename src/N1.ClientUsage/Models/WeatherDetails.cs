using System.Text.Json.Serialization;

namespace N1.ClientUsage.Models;

public class WeatherDetails
{
    [JsonPropertyName("location")]
    public Location Location { get; set; }

    [JsonPropertyName("current")]
    public Current Current { get; set; }
}