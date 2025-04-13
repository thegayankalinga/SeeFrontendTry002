using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BackendType
{
    [EnumMember(Value = "java")]
    [Display(Name = "Java")]
    Java, 
}