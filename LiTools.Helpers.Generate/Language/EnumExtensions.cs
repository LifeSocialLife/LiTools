// <summary>
// Langes Enums Codes.
// </summary>
// <copyright file="EnumExtensions.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (Lempa)</author>

namespace LiTools.Helpers.Generate.Language
{
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// Get Language nama from description.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// <see langword="public"/> static string GetDescription(this LanguageCode lang).
        /// </summary>
        /// <param name="lang">LanguageCode enum.</param>
        /// <returns>Language name as string.</returns>
        public static string GetDescription(this LanguageCodesEnums lang)
        {
            var fieldInfo = lang.GetType().GetField(lang.ToString());
            if (fieldInfo == null)
            {
                return string.Empty;
            }

            var descriptionAttribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return descriptionAttribute?.Description ?? lang.ToString();
        }
    }
}
