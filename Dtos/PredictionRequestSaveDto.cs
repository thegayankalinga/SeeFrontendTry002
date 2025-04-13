using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Dtos;

public class PredictionRequestSaveDto
{
    
    public required string ProjectName { get; set; }
    public required Region Region { get; set; }
    public required FeatureDataSaveRequestDto FeatureData { get; set; }

}