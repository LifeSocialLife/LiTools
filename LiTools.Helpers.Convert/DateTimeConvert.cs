// <summary>
// DateTimeConvert.
// </summary>
// <copyright file="DateTimeConvert.cs" company="LiSoLi">
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
    using System.Runtime.Serialization;
    using System.Text;
    using TimeZoneConverter;

    /// <summary>
    /// DateTime Convert.
    /// </summary>
    public static class DateTimeConvert
    {
        /// <summary>
        /// Get time zone from Iana zones.
        /// </summary>
        /// <param name="zone">IanaTimeZonesEnum.</param>
        /// <returns>TimeZoneInfo.</returns>
        public static TimeZoneInfo GetTimeZoneByIana(IanaTimeZonesEnum zone)
        {
            /*
                var timeZoneData = TZConvert.GetTimeZoneInfo(zone.ToString());
                return timeZoneData;
                var timeZoneData = TZConvert.GetTimeZoneInfo("Europe/Stockholm");
            */
            try
            {
                var ianaString = zone.GetIanaString();
                return TZConvert.GetTimeZoneInfo(ianaString);
            }
            catch (TimeZoneNotFoundException)
            {
                // If the IANA time zone is not found, return UTC as a fallback
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif
                return TimeZoneInfo.Utc;
            }
            catch (InvalidTimeZoneException)
            {
                // If the IANA time zone is invalid, return UTC as a fallback
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif
                return TimeZoneInfo.Utc;
            }
        }

        /// <summary>
        /// Get Utc time from local time whit datetime and TimeZoneInfo.
        /// </summary>
        /// <param name="dateTime">DateTime.</param>
        /// <param name="timeZone">TimeZoneInfo.</param>
        /// <returns>UTC time.</returns>
        public static DateTime ToUtcFromTimeZone(DateTime dateTime, TimeZoneInfo timeZone)
        {
            // Convert the local time to UTC
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone);
        }

        /// <summary>
        /// Turn Iana zone into Display name. ex America_New_York into America/New York.
        /// </summary>
        /// <param name="zone">IanaTimeZonesEnum.</param>
        /// <returns>ex America_New_York into America/New York.</returns>
        public static string IanaToDisplayName(this IanaTimeZonesEnum zone)
        {
            // Convert enum name like America_New_York → "America/New York"
            var parts = zone.ToString().Split('_');
            if (parts.Length == 1)
            {
                return parts[0];
            }

            var display = string.Join('/', parts[..^1]) + " " + parts[^1];
            return display;
        }

        /// <summary>
        /// Convert Datetime value to string. Only return date as yyyyMMdd.
        /// </summary>
        /// <param name="dt">datetime value.</param>
        /// <returns>date as string yyyyMMdd.</returns>
        public static (bool Error, string DateAsString) DateToString(DateTime dt)
        {
            try
            {
                string tmpDate = dt.ToString("yyyyMMdd");
                return (false, tmpDate);
            }
            catch
            {
                return (false, string.Empty);
            }
        }

        /// <summary>
        /// String Y, M, D into datetime UTC.
        /// </summary>
        /// <param name="year">Y.</param>
        /// <param name="mounth">M.</param>
        /// <param name="day">D.</param>
        /// <returns>bool Error, DateTime Date as UTC time.</returns>
        public static (bool Error, DateTime Date) StringToDateUtcFirstTimeOfDay(string year, string mounth, string day)
        {
            try
            {
                DateTime tmpDate = Convert.ToDateTime($"{year}-{mounth}-{day}T00:00:00.000+00:00");
                return (false, tmpDate);
            }
            catch
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                return (true, DateTime.UtcNow);
            }
        }

        /// <summary>
        /// Pnr as string to datetime UTC. return null if error. format '1900-01-01' or '19000101' or '19000101-0000'.
        /// </summary>
        /// <param name="input">pnr as string in this format, '1900-01-01' or '19000101' or '19000101-0000'.</param>
        /// <returns>datetime or null.</returns>
        public static DateTime? PnrAsStringToDateTimeUtc(string input)
        {
            /*
            DateTime? parsedDate = PnrAsStringToDateTimeUtc("1979-07-30");

            if (parsedDate.HasValue)
            {
                Console.WriteLine($"The parsed UTC date is: {parsedDate.Value:yyyy-MM-ddTHH:mm:ssZ}");
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
            */

            // Extract the date part based on the format
            string datePart = input.Contains("-") && input.Length == 10
                ? input.Substring(0, 10) // Extract YYYY-MM-DD
                : input.Length >= 8
                    ? input.Substring(0, 8) // Extract YYYYMMDD
                    : string.Empty;

            // Validate the extracted date
            if (DateTime.TryParseExact(
                datePart,
                new[] { "yyyy-MM-dd", "yyyyMMdd" },
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out DateTime parsedDate))
            {
                return parsedDate;
            }

            return null; // Return null if the date is invalid
        }

        /// <summary>
        /// String date as 20220101 into datetime UTC.
        /// </summary>
        /// <param name="dateOnly">Y.</param>
        /// <returns>bool Error, DateTime Date as UTC time.</returns>
        public static (bool Error, DateTime Date) StringToDateUtcFirstTimeOfDay(string dateOnly)
        {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            string zzDebug = "dsfdsf";
#pragma warning restore CS0219 // Variable is assigned but its value is never used

            if (string.IsNullOrEmpty(dateOnly))
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                return (true, GetDefaultDateAsUtc());
            }

            // Do we have - inside date string.
            if (!dateOnly.Contains("-"))
            {
                // Date dont have - in date. ex 2022-10-22. i think the line is 20221022.. Rebuild syntax.
                // lengt shod be 8 whitout -. check!.
                if (dateOnly.Length != 8)
                {
                    // Lengt is not 8. it shod be 8.
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }

                    return (true, GetDefaultDateAsUtc());
                }

#pragma warning disable CS0168 // Variable is declared but never used
                try
                {
                    string year = dateOnly.Substring(0, 4);
                    string mounth = dateOnly.Substring(4, 2);
                    string day = dateOnly.Substring(6, 2);

                    // dateOnly = $"{year}-{mounth}-{day}";
                    return StringToDateUtcFirstTimeOfDay(year, mounth, day);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    // Error e.
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }

                    return (true, GetDefaultDateAsUtc());
                }
#pragma warning restore CS0168 // Variable is declared but never used
            }

            // New code. check that code is working - 2022-10-22
            zzDebug = "dfdf";

            // We have - inside string. Check that is is 10 lengt. 4-2-2 + 2(-) ( 2010-01-01 )
            if (dateOnly.Length != 10)
            {
                // Lengt is not 10. it shod be 8.
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                return (true, GetDefaultDateAsUtc());
            }

            var dd = dateOnly.Split('-');
            if (dd.Length != 3)
            {
                // we shod have 3 in lengt. not this.
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                return (true, GetDefaultDateAsUtc());
            }

            zzDebug = "sdfdf";

            return StringToDateUtcFirstTimeOfDay(dd[0], dd[1], dd[2]);
        }

        /// <summary>
        /// Get datetime as UTC in 2000-01-01 00:00:00 UTC time.
        /// </summary>
        /// <returns>datetime 2000-01-01 00:00:00 UTC.</returns>
        public static DateTime GetDefaultDateAsUtc()
        {
            return Convert.ToDateTime($"2000-01-01T00:00:00.000+00:00");
        }

        /// <summary>
        /// Get default date 2000-01-01 whit own hour and min data. Return as UTC datetime.
        /// </summary>
        /// <param name="h">Hour.</param>
        /// <param name="m">Minutes.</param>
        /// <returns>Datetime UTC.</returns>
        public static DateTime GetDefaultDateAsUtc(string h, string m)
        {
            if (h.Length == 1)
            {
                h = $"0{h}";
            }

            if (m.Length == 1)
            {
                m = $"0{m}";
            }

            return Convert.ToDateTime($"2000-01-01T{h}:{m}:00.000+00:00");
        }

        /*
        this.DtFirstDataExist = Convert.ToDateTime("2000-01-01T00:00:00.000+00:00"); // DateTime.UtcNow;
        this.DtLastDataExist = Convert.ToDateTime("2000-01-01T00:00:00.000+00:00"); // DateTime.UtcNow;
         * */
    }
}
