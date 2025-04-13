using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Models;

public class PredictionResult
{
    [Key]
    public int ResultId { get; set; }
    public float DeliveryEffort { get; set; }
    public float EngineeringEffort { get; set; }
    public float DevOpsEffort { get; set; }
    public float QaEffort { get; set; }
    public required PredictionModel ModelName { get; set; } 
    
    //one to many
    [ForeignKey("ProjectId")]
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    
    [ForeignKey("FeatureSetId")]
    public int FeatureSetId { get; set; }
    public FeatureData? FeatureData { get; set; }
}