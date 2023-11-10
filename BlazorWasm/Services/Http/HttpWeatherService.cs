using System.Net.Http.Headers;
using System.Text.Json;
using Shared.Models;

namespace BlazorWasm.Services.Http;

public class HttpWeatherService : IWeatherService
{

    private readonly HttpClient client;
    private readonly IAuthService authService;

    public HttpWeatherService(HttpClient client, IAuthService authService)
    {
        this.client = client;
        this.authService = authService;
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeather()
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authService.Jwt);
        HttpResponseMessage response = await client.GetAsync("https://localhost:7130/WeatherForecast");
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        IEnumerable<WeatherForecast>? weatherForecasts = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return weatherForecasts!;
    }
}