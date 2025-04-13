using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Region
{
    [EnumMember(Value = "gcc")]
    [Display(Name = "Gulf Cooperation Council")]
    Gcc,

    [EnumMember(Value = "apac")]
    [Display(Name = "Asia Pacific")]
    Apac,

    [EnumMember(Value = "asia")]
    [Display(Name = "Asia")]
    Asia,

    [EnumMember(Value = "north-america")]
    [Display(Name = "North America")]
    NorthAmerica,

    [EnumMember(Value = "europe")]
    [Display(Name = "Europe")]
    Europe,

    [EnumMember(Value = "africa")]
    [Display(Name = "Africa")]
    Africa
}

// In Razor file
// @model Region
//
//     <p>Region: @Model.GetDisplayName()</p>