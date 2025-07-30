using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
    }
}
