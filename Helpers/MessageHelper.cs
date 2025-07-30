using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CommonSolution.Helpers
{
    public static class MessageHelper
    {
        /// <summary>
        /// Gera mensagem com base em enum e recurso associado ao DisplayAttribute.
        /// </summary>
        public static string FormatFromEnum<TEnum>(TEnum enumValue, Func<string, string> resourceAccessor, string template)
            where TEnum : Enum
        {
            var displayAttr = enumValue.GetType()
                .GetField(enumValue.ToString())?
                .GetCustomAttribute<DisplayAttribute>();

            if (displayAttr == null)
                throw new InvalidOperationException($"Enum {enumValue} não possui DisplayAttribute.");

            var name = displayAttr.GetName(); // "HWB0001", etc.
            var displayValue = resourceAccessor(name); // HotmartMessages.HWB0001, etc.

            return string.Format(template, displayValue);
        }
    }
}
