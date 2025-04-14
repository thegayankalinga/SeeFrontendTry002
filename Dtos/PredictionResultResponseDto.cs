using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Dtos;

public class PredictionResultResponseDto
{
 
    public int ResultId { get; set; }
    
    public float DeliveryEffort { get; set; }
    
    public float EngineeringEffort { get; set; }
    
    public float DevOpsEffort { get; set; }
    
    public float QaEffort { get; set; }
    
    public PredictionModel PredictionModel { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public float TotalEffort => DeliveryEffort + EngineeringEffort + DevOpsEffort + QaEffort;
}