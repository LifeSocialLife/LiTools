// <summary>
// Iana TimeZones Enum Extensions.
// </summary>
// <copyright file="IanaTimeZonesEnumExtensions.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;
    using TimeZoneConverter;

    /// <summary>
    /// Iana TimeZones Enum Extensions.
    /// </summary>
    public static class IanaTimeZonesEnumExtensions
    {
        /// <summary>
        /// Get the EnumMember value for the IanaTimeZonesEnum.
        /// </summary>
        /// <param name="zone">IanaTimeZonesEnum.</param>
        /// <returns>EnumMember value as a string.</returns>
        public static string GetIanaString(this IanaTimeZonesEnum zone)
        {
            var type = typeof(IanaTimeZonesEnum);
            var member = type.GetMember(zone.ToString()).FirstOrDefault();
            var attribute = member?.GetCustomAttribute<EnumMemberAttribute>();

            return attribute?.Value ?? zone.ToString(); // fallback to enum name
        }
    }
}
