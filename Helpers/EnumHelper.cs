using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CommonSolution.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<TEnum>(TEnum value) where TEnum : Enum
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }

        public static string GetDisplayText<TEnum>(TEnum value) where TEnum : Enum
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DisplayAttribute>();

            if (attr == null)
                throw new InvalidOperationException($"Enum '{value}' não possui DisplayAttribute.");

            if (attr.ResourceType != null && !string.IsNullOrEmpty(attr.Name))
            {
                var property = attr.ResourceType.GetProperty(attr.Name,
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                if (property != null)
                    return property.GetValue(null)?.ToString() ?? attr.Name;
            }

            return attr.Name ?? value.ToString();
        }

        public static bool TryParseEnum<TEnum>(string value, out TEnum result, bool ignoreCase = false)
        where TEnum : struct, Enum
        {
            return Enum.TryParse(value, ignoreCase, out result) && Enum.IsDefined(typeof(TEnum), result);
        }
    }
}
