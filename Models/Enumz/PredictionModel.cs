using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SeeFrontendTry002.Models.Enumz;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PredictionModel
{
    [EnumMember(Value = "XGBoost")]
    [Display(Name = "XGBoost")]
    XGBoost,
    
    [EnumMember(Value = "RandomForest")]
    [Display(Name = "RandomForest")]
    RandomForest,
    
    [EnumMember(Value = "LSTM")]
    [Display(Name = "LSTM")]
    LSTM,
    
    [EnumMember(Value = "MLP")]
    [Display(Name = "MLP")]
    MLP,
    
    [EnumMember(Value = "Hybrid")]
    [Display(Name = "Hybrid")]
    Hybrid
}