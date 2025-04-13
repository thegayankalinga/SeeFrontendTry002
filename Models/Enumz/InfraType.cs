using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum InfraType
{
    [EnumMember(Value = "on-premises")]
    [Display(Name = "On-Premises")]
    OnPremises,

    [EnumMember(Value = "aws")]
    [Display(Name = "Amazon Web Services")]
    AWS,

    [EnumMember(Value = "azure")]
    [Display(Name = "Microsoft Azure")]
    Azure,

    [EnumMember(Value = "gcp")]
    [Display(Name = "Google Cloud Platform")]
    GCP 
}