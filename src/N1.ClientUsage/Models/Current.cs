using System.Text.Json.Serialization;

namespace N1.ClientUsage.Models;

public class Current
{
    [JsonPropertyName("last_updated_epoch")]
    public long LastUpdatedEpoch { get; set; }

    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; set; }

    [JsonPropertyName("temp_c")]
    public double TemperatureCelsius { get; set; }

    [JsonPropertyName("temp_f")]
    public double TemperatureFahrenheit { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("condition")]
    public Condition Condition { get; set; }

    [JsonPropertyName("wind_mph")]
    public double WindMph { get; set; }

    [JsonPropertyName("wind_kph")]
    public double WindKph { get; set; }

    [JsonPropertyName("wind_degree")]
    public int WindDegree { get; set; }

    [JsonPropertyName("wind_dir")]
    public string WindDirection { get; set; }

    [JsonPropertyName("pressure_mb")]
    public double PressureMb { get; set; }

    [JsonPropertyName("pressure_in")]
    public double PressureIn { get; set; }

    [JsonPropertyName("precip_mm")]
    public double PrecipitationMm { get; set; }

    [JsonPropertyName("precip_in")]
    public double PrecipitationIn { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("cloud")]
    public int Cloud { get; set; }

    [JsonPropertyName("feelslike_c")]
    public double FeelsLikeCelsius { get; set; }

    [JsonPropertyName("feelslike_f")]
    public double FeelsLikeFahrenheit { get; set; }

    [JsonPropertyName("vis_km")]
    public double VisibilityKm { get; set; }

    [JsonPropertyName("vis_miles")]
    public double VisibilityMiles { get; set; }

    [JsonPropertyName("uv")]
    public double Uv { get; set; }

    [JsonPropertyName("gust_mph")]
    public double GustMph { get; set; }

    [JsonPropertyName("gust_kph")]
    public double GustKph { get; set; }

    [JsonPropertyName("air_quality")]
    public AirQuality AirQuality { get; set; }
}