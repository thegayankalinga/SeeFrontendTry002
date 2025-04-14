using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CalculationStatusType
{
    [EnumMember(Value = "pending")]
    [Display(Name = "Pending")]
    Pending, 
    
    [EnumMember(Value = "success")]
    [Display(Name = "Success")]
    Success, 
    
    [EnumMember(Value = "failed")]
    [Display(Name = "Failed")]
    Failed, 
}
