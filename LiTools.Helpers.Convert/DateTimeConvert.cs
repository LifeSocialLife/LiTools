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

        public static (bool Error, DateTime Date) StringToDateUtcFirstTimeOfDay(string year, string mounth, string day)
        {
            try
            {
                DateTime tmpDate = Convert.ToDateTime($"{year}-{mounth}-{day}T00:00:00.000+00:00");
                return (false, tmpDate);
            }
            catch
            {
                return (true, DateTime.UtcNow);
            }
        }

        //public static (bool Error, DateTime DateTime) StringToDateTimeUtc(string dt)
        //{
        //    try
        //    {
        //        DateTime tmpDate = Convert.ToDateTime("2000-01-01T00:00:00.000+00:00");
        //        return (false, tmpDate);
        //    }
        //    catch
        //    {
        //        return (true, DateTime.UtcNow);
        //    }
        //}

        /*
         *             this.DtFirstDataExist = Convert.ToDateTime("2000-01-01T00:00:00.000+00:00"); // DateTime.UtcNow;
            this.DtLastDataExist = Convert.ToDateTime("2000-01-01T00:00:00.000+00:00"); // DateTime.UtcNow;
         * */
    }
}
