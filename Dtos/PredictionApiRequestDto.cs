using System.Text.Json.Serialization;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Dtos;

public class PredictionApiRequestDto
{
    [JsonPropertyName("features")]
    public required List<string> Features { get; set; }
    
    [JsonPropertyName("model_name")]
    public required string ModelName { get; set; }
}