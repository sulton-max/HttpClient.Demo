using N1.ClientUsage.Configurations;
using N1.ClientUsage.Models;

// Third Party Api
// Azure Blob Storage - service for storing files


// why - 
// what - 
// how - 
// where - gRPC

// var httpClient = new HttpClient();
//
// // HTTP - verb, url, headers, body, status code, response body, timeout
// httpClient.Timeout = TimeSpan.FromSeconds(5);
// httpClient.BaseAddress = new Uri("https://api.weatherapi.com");
//
// var result = await 
// httpClient.GetFromJsonAsync<WeatherDetails>("v1/current.json?q=Tashkent&aqi=yes&key=c4b5b99def884f31a23132508243101");
//
// Console.ReadLine();

var builder = WebApplication.CreateBuilder(args);

await builder.ConfigureAsync();

var app = builder.Build();

await app.ConfigureAsync();
await app.RunAsync();