using SeeFrontendTry002.Dtos;

namespace SeeFrontendTry002.ViewModels;

public class ResultViewModel
{
    public int ProjectId { get; set; }
        
    public int FeaturesetId { get; set; }
    
    public required PredictionInputViewModel PredictionInputViewModel { get; set; }
        
    public string? ProjectName { get; set; }
        
    public List<PredictionResultResponseDto>? Results { get; set; }
        
    public bool Success { get; set; }
        
    public string ErrorMessage { get; set; }  = string.Empty;
}