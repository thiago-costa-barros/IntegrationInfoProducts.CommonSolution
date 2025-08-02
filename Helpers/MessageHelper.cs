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
        public static string FormatFromEnum<TEnum>(TEnum enumValue, string template)
            where TEnum : Enum
        {
            var display = EnumHelper.GetDisplayText(enumValue);
            return string.Format(template, display);
        }

        public static string GetExceptionMessage(string resourceKey, params object[] args)
        {
            var resourceManager = Resources.ExceptionMessages.ResourceManager;
            var message = resourceManager.GetString(resourceKey) ?? "Mensagem não encontrada.";
            return string.Format(message, args);
        }
    }
}
