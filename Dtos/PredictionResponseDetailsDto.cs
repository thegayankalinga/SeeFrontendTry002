using SeeFrontendTry002.Models;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Dtos;

public class PredictionResponseDetailsDto
{

    public int ProjectId { get; set; }
    public required string ProjectName { get; set; }
    public required Region Region { get; set; }
    
    public CalculationStatusType CalculationStatus { get; set; }
    
    public required FeatureDataResponseDto FeatureData { get; set; }
    
    //One to many lists
    public ICollection<PredictionResult>? PredictionResultsFromProjects { get; set; }
}