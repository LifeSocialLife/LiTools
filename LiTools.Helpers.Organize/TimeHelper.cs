// <summary>
// Time helper-
// </summary>
// <copyright file="TimeHelper.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Organize
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// Time intervals.
    /// </summary>
    public enum TimeValuesEnum
    {
        /// <summary>Years.</summary>
        Years = 0,

        /// <summary>Months.</summary>
        Months = 1,

        /// <summary>Weeks.</summary>
        Weeks = 2,

        /// <summary>Days.</summary>
        Days = 3,

        /// <summary>Hours.</summary>
        Hours = 4,

        /// <summary>Minutes.</summary>
        Minutes = 5,

        /// <summary>Seconds.</summary>
        Seconds = 6,

        /// <summary>Milliseconds.</summary>
        Milliseconds = 7,
    }

    /// <summary>
    /// Timers Helper.
    /// </summary>
    public static class TimeHelper
    {
        private static string? zzDebug { get; set; }

        /// <summary>
        /// TimeShodTrigger.
        /// </summary>
        /// <param name="lastrun">datetime of last run.</param>
        /// <param name="time">Time value to trigger on.</param>
        /// <param name="interval">time interval in minutes.</param>
        /// <returns>True if time has ended. false if time not have ended.</returns>
        public static bool TimeShodTrigger(DateTime lastrun, TimeValuesEnum time, ushort interval)
        {
            if (time != TimeValuesEnum.Days &&
                time != TimeValuesEnum.Hours &&
                time != TimeValuesEnum.Minutes &&
                time != TimeValuesEnum.Seconds &&
                time != TimeValuesEnum.Milliseconds)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                throw new NotImplementedException();
            }

            if (time == TimeValuesEnum.Days && (DateTime.UtcNow - lastrun).TotalDays >= interval)
            {
                return true;
            }
            else if (time == TimeValuesEnum.Hours && (DateTime.UtcNow - lastrun).TotalHours >= interval)
            {
                return true;
            }
            else if (time == TimeValuesEnum.Minutes && (DateTime.UtcNow - lastrun).TotalMinutes >= interval)
            {
                return true;
            }
            else if (time == TimeValuesEnum.Seconds && (DateTime.UtcNow - lastrun).TotalSeconds >= interval)
            {
                return true;
            }
            else if (time == TimeValuesEnum.Milliseconds && (DateTime.UtcNow - lastrun).TotalMilliseconds >= interval)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Split milliseconds value into {value}:{value}:{value} ex: 1:5:10 = 1 Hours, 5 Minutes, 10 seconds.
        /// </summary>
        /// <param name="reportMax">TimeValuesEnum what will be the start of repport.</param>
        /// <param name="millisec">input value to calc from.</param>
        /// <returns>1:5:10 = 1 Hours, 5 Minutes, 10 seconds.</returns>
        /// <exception cref="NotImplementedException">Error.</exception>
        public static string MillisecondsSplitIntoStringRepport(TimeValuesEnum reportMax, double millisec)
        {
            // Check if we support timmeValuesEnum.
            if (reportMax != TimeValuesEnum.Weeks &&
                reportMax != TimeValuesEnum.Days &&
                reportMax != TimeValuesEnum.Hours &&
                reportMax != TimeValuesEnum.Minutes &&
                reportMax != TimeValuesEnum.Seconds)
            {
                // We dont support this.
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                throw new NotImplementedException();
            }

            string tmpReturn = string.Empty;
            uint tmpWeekCount = 0;

            /*
            uint tmpDayCount = 0;
            uint tmpHoursCount = 0;
            uint tmpMinutesCount = 0;
            uint tmpSecondsCount = 0;
            */

            double tmpTimeLeftInSec = millisec / 1000;
            bool tmpDoNext = false;

            if (reportMax == TimeValuesEnum.Weeks)
            {
                while (true)
                {
                    // Weeks as max value. 1 week = 604800 sec in one week.
                    TimeSpan time = TimeSpan.FromSeconds(tmpTimeLeftInSec);

                    if (time.Days > 7)
                    {
                        tmpWeekCount++;
                        tmpTimeLeftInSec -= 604800;
                    }
                    else
                    {
                        break;
                    }
                }

                tmpDoNext = true;
                tmpReturn = $"{tmpWeekCount}:";
            }

            if (reportMax == TimeValuesEnum.Days || tmpDoNext)
            {
                TimeSpan time = TimeSpan.FromSeconds(tmpTimeLeftInSec);
                tmpReturn += $"{time.Days}:";
                tmpDoNext = true;
            }

            if (reportMax == TimeValuesEnum.Hours || tmpDoNext)
            {
                TimeSpan time = TimeSpan.FromSeconds(tmpTimeLeftInSec);
                tmpReturn += $"{time.Hours}:";
                tmpDoNext = true;
            }

            if (reportMax == TimeValuesEnum.Minutes || tmpDoNext)
            {
                TimeSpan time = TimeSpan.FromSeconds(tmpTimeLeftInSec);
                tmpReturn += $"{time.Minutes}:";
                tmpDoNext = true;
            }

            if (reportMax == TimeValuesEnum.Seconds || tmpDoNext)
            {
                TimeSpan time = TimeSpan.FromSeconds(tmpTimeLeftInSec);
                tmpReturn += $"{time.Seconds}";

                // tmpDoNext = true;
            }

            zzDebug = "dsfdsf";

            return tmpReturn;
        }

        /// <summary>
        /// Split milliseconds value into nice text return  ex: 0 Weeks 0 Days 0 Hours 5 Min 0 Sec.
        /// </summary>
        /// <param name="reportMax">TimeValuesEnum what will be the start of repport.</param>
        /// <param name="millisec">input value to calc from.</param>
        /// <param name="repportEmpty">Include allzero values also.</param>
        /// <returns>0 Weeks 0 Days 0 Hours 5 Min 0 Sec.</returns>
        public static string MillisecondsStringRepportIntoNiceTextReturn(TimeValuesEnum reportMax, double millisec, bool repportEmpty = false)
        {
            var aa = MillisecondsSplitIntoStringRepport(reportMax, millisec);
            return MillisecondsStringRepportIntoNiceTextReturn(aa, repportEmpty);
        }

        /// <summary>
        /// Turn stringRepport into nice text return  ex: 0 Weeks 0 Days 0 Hours 5 Min 0 Sec.
        /// </summary>
        /// <param name="stringRepport">Input time data as string repport.</param>
        /// <param name="repportEmpty">Include allzero values also.</param>
        /// <returns>0 Weeks 0 Days 0 Hours 5 Min 0 Sec.</returns>
        /// <exception cref="NotImplementedException">Error.</exception>
        public static string MillisecondsStringRepportIntoNiceTextReturn(string stringRepport, bool repportEmpty = false)
        {
            if (stringRepport == null)
            {
                return "Error NULL";
            }

            if (!stringRepport.Contains(":"))
            {
                return "Error :";
            }

            var data = stringRepport.Split(":", StringSplitOptions.RemoveEmptyEntries);

            zzDebug = "dfsdf";

            string tmpReturn = string.Empty;
            string tmpWeekCount = "0";
            string tmpDayCount = "0";
            string tmpHoursCount = "0";
            string tmpMinutesCount = "0";
            string tmpSecondsCount = "0";

            if (data.Length == 5)
            {
                tmpWeekCount = data[0];
                tmpDayCount = data[1];
                tmpHoursCount = data[2];
                tmpMinutesCount = data[3];
                tmpSecondsCount = data[4];
            }
            else if (data.Length == 4)
            {
                tmpDayCount = data[0];
                tmpHoursCount = data[1];
                tmpMinutesCount = data[2];
                tmpSecondsCount = data[3];
            }
            else if (data.Length == 3)
            {
                tmpHoursCount = data[0];
                tmpMinutesCount = data[1];
                tmpSecondsCount = data[2];
            }
            else if (data.Length == 2)
            {
                tmpMinutesCount = data[0];
                tmpSecondsCount = data[1];
            }
            else if (data.Length == 1)
            {
                tmpSecondsCount = data[0];
            }
            else
            {
                // We dont support this.
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                throw new NotImplementedException();
            }

            if ((!string.IsNullOrEmpty(tmpWeekCount) && tmpWeekCount != "0") || repportEmpty)
            {
                tmpReturn += $" {tmpWeekCount} Weeks";
            }

            if ((!string.IsNullOrEmpty(tmpDayCount) && tmpDayCount != "0") || repportEmpty)
            {
                tmpReturn += $" {tmpDayCount} Days";
            }

            if ((!string.IsNullOrEmpty(tmpHoursCount) && tmpHoursCount != "0") || repportEmpty)
            {
                tmpReturn += $" {tmpHoursCount} Hours";
            }

            if ((!string.IsNullOrEmpty(tmpMinutesCount) && tmpMinutesCount != "0") || repportEmpty)
            {
                tmpReturn += $" {tmpMinutesCount} Min";
            }

            if ((!string.IsNullOrEmpty(tmpSecondsCount) && tmpSecondsCount != "0") || repportEmpty)
            {
                tmpReturn += $" {tmpSecondsCount} Sec";
            }

            return tmpReturn;
        }

        /// <summary>
        /// Timespan into Nice Text. using timedata.utc as time base.
        /// </summary>
        /// <param name="date">datetime.</param>
        /// <returns>Time as nice text.</returns>
        public static string TimespanToNiceText(DateTime date)
        {
            TimeSpan aa = DateTime.UtcNow - date;
            return TimespanToNiceText(aa);
        }

        /// <summary>
        /// Timespan into Nice Text.
        /// </summary>
        /// <param name="data">timepan.</param>
        /// <returns>Time as nice text.</returns>
        public static string TimespanToNiceText(TimeSpan data)
        {
            if (data == null)
            {
                return "error";
            }

            string tmpReturn = string.Empty;

            // more then 60 sec
            if (data.TotalSeconds > 60)
            {
                // More then 60 min.
                if (data.TotalMinutes > 60)
                {
                    // More then 24 hours ago.
                    if (data.TotalHours > 24)
                    {
                        tmpReturn += $"{data.Days} Days ";
                    }

                    tmpReturn += $"{data.Hours} Hours ";
                }

                tmpReturn += $"{data.Minutes} Min ";
            }

            tmpReturn += $"{data.Seconds} Sec";

            return tmpReturn;
        }
    }
}
