using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RatingType
{
    [EnumMember(Value = "high")]
    [Display(Name = "High")]
    High,
    
    [EnumMember(Value = "medium")]
    [Display(Name = "Medium")]
    Medium,
    
    [EnumMember(Value = "low")]
    [Display(Name = "Low")]
    Low,
}