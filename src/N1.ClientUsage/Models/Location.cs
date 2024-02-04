using System.Text.Json.Serialization;

namespace N1.ClientUsage.Models;

public class Location
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lon")]
    public double Longitude { get; set; }

    [JsonPropertyName("tz_id")]
    public string TimeZoneId { get; set; }

    [JsonPropertyName("localtime_epoch")]
    public long LocalTimeEpoch { get; set; }

    [JsonPropertyName("localtime")]
    public string LocalTime { get; set; }
}