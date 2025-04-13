using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DatabaseType
{
    [EnumMember(Value = "postgresql")]
    [Display(Name = "PostgresSQL")]
    PostgresSql,

    [EnumMember(Value = "mysql")]
    [Display(Name = "MySQL")]
    MySql,

    [EnumMember(Value = "oracle")]
    [Display(Name = "Oracle Database")]
    Oracle  
}