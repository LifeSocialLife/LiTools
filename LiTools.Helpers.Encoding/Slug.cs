// <summary>
// Slug Generator helper.
// </summary>
// <copyright file="Slug.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (Lempa)</author>

namespace LiTools.Helpers.Encoding
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Slug generator.
    /// </summary>
    public static class Slug
    {
        /// <summary>
        /// Generate a slug from a string. only for Western languages, typically languages that use the Latin alphabet.
        /// </summary>
        /// <param name="name">string to convert into slug.</param>
        /// <returns>slug as string.</returns>
        public static string GenerateSlug(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            // Convert to lowercase
            string slug = name.ToLowerInvariant();

            // Replace accented characters with their unaccented equivalents
            slug = RemoveAccents(slug);

            // Replace '&' with 'and'
            slug = slug.Replace("&", "and");

            // Replace spaces and invalid characters with hyphens
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", string.Empty); // Remove invalid chars
            slug = Regex.Replace(slug, @"\s+", "-");         // Replace spaces with hyphen
            slug = Regex.Replace(slug, @"-+", "-");          // Ensure no consecutive hyphens

            // Trim leading and trailing hyphens
            slug = slug.Trim('-');

            return slug;
        }

        private static string RemoveAccents(string text)
        {
            // Normalize the text to decompose characters
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            // Iterate through characters and keep only non-diacritic ones
            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}