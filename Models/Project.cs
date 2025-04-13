using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Models;

public class Project
{
    [Key]
    public int ProjectId { get; set; }
    
    [MaxLength(100)]
    public required string ProjectName { get; set; }
    
    public required Region Region { get; set; }
    
    // FK to AppUser
    [MaxLength(450)]
    public string? ProjectManagerId { get; set; }
    [ForeignKey("ProjectManagerId")]
    public AppUser? ProjectManager { get; set; }

    [MaxLength(450)]
    public string? PreSalesEngineerId { get; set; }
    [ForeignKey("PreSalesEngineerId")]
    public AppUser? PreSalesEngineer { get; set; }
    
    //Navigation Properties (One to one Relationship.)
    public FeatureData? FeatureData { get; set; }
    
    //Navigation One to Many
    public ICollection<PredictionResult>? PredictionResults { get; set; }
}