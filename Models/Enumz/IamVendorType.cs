using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IamVendorType
{
    [EnumMember(Value = "custom")]
    [Display(Name = "Custom IAM")]
    Custom,

    [EnumMember(Value = "keycloak")]
    [Display(Name = "Keycloak")]
    Keycloak,

    [EnumMember(Value = "okta")]
    [Display(Name = "Okta")]
    Okta
}