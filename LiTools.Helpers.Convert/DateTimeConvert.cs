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
    using System.Text;

    /// <summary>
    /// DateTime Convert.
    /// </summary>
    public static class DateTimeConvert
    {
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
