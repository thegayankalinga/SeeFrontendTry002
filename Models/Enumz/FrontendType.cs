using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FrontendType
{
    [EnumMember(Value = "reactjs")]
    [Display(Name = "ReactJs")]
    ReactJs, 
}