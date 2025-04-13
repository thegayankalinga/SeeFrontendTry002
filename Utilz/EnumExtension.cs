using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SeeFrontendTry002.Utilz;

public static class EnumExtension
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue
            .GetType()
            .GetMember(enumValue.ToString())[0]
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName() ?? enumValue.ToString();
    }
}