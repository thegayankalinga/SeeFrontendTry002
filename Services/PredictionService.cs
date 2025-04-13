using System.Text;
using System.Text.Json;
using SeeFrontendTry002.Dtos;
using SeeFrontendTry002.Interface;

namespace SeeFrontendTry002.Services;

public class PredictionService : IPredictionService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PredictionService> _logger;
    private const string PredictionUrl = "http://192.168.1.81:8000/predict";

    public PredictionService(HttpClient httpClient, ILogger<PredictionService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<PredictionApiResponseDto?> PredictAsync(PredictionApiRequestDto apiRequest)
    {
        var json = JsonSerializer.Serialize(apiRequest);
        _logger.LogInformation($"Prediction Request: {json}");

        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(PredictionUrl, content);
        response.EnsureSuccessStatusCode();
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PredictionApiResponseDto>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true 
            
        });
    }
}