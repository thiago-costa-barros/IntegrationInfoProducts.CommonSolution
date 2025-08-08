using System.Reflection;
using System.Text.Json.Serialization;

namespace CommonSolution.Helpers;
public static class ValidationHelper
{
    public static string GetJsonPropertyName<T>(string propertyName)
    {
        var prop = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (prop == null) return propertyName;

        var jsonAttr = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
        return jsonAttr?.Name ?? propertyName;
    }
}