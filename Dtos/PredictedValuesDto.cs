using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Dtos;

public class PredictedValuesDto
{
    [JsonPropertyName("Delivery Effort")]
    public float DeliveryEffort { get; set; }

    [JsonPropertyName("Engineering Effort")]
    public float EngineeringEffort { get; set; }

    [JsonPropertyName("DevOps Effort")]
    public float DevOpsEffort { get; set; }

    [JsonPropertyName("QA Effort")]
    public float QaEffort { get; set; }
}