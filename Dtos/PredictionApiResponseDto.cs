namespace SeeFrontendTry002.Dtos;

public class PredictionApiResponseDto
{
    public bool Success { get; set; }
    public required string Model { get; set; }
    public required PredictedValuesDto Predictions { get; set; } 
}