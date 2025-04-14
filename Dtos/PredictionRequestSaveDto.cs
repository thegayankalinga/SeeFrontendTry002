using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Dtos;

public class PredictionRequestSaveDto
{
    
    public required string ProjectName { get; set; }
    public required Region Region { get; set; }

    public CalculationStatusType CalculationStatus { get; set; } = CalculationStatusType.Pending;
    public required FeatureDataSaveRequestDto FeatureData { get; set; }

}