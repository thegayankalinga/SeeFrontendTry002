using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MobileType
{
    [EnumMember(Value = "flutter")]
    [Display(Name = "Flutter")]
    Flutter 
}