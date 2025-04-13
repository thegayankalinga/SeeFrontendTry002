using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SpeedType
{
    [EnumMember(Value = "fast")]
    [Display(Name = "Fast")]
    Fast,
    
    [EnumMember(Value = "moderate")]
    [Display(Name = "Moderate")]
    Moderate,
    
    [EnumMember(Value = "slow")]
    [Display(Name = "Slow")]
    Slow,
}