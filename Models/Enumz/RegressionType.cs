using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RegressionType
{
    [EnumMember(Value = "full")]
    [Display(Name = "Full Regression")]
    FullRegression,
    
    [EnumMember(Value = "partial")]
    [Display(Name = "Partial Regression")]
    PartialRegression, 
}