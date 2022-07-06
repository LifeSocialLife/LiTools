// <summary>
// Check string data.
// </summary>
// <copyright file="DateTimeChecks.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Check
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// DateTime checks.
    /// </summary>
    public static class DateTimeChecks
    {
        /// <summary>
        /// Check if datetime is today. use datetime.utcnow as referanse.
        /// </summary>
        /// <param name="datetmp">datetime to check.</param>
        /// <returns>True or false.</returns>
        public static bool DateIsToday(DateTime datetmp)
        {
            return DateIsToday(datetmp, DateTime.UtcNow);
        }

        /// <summary>
        /// Check if datetime is today.
        /// </summary>
        /// <param name="datetmp">Datetime to check.</param>
        /// <param name="dtCompareTo">Datetime to compare to.</param>
        /// <returns>True or false.</returns>
        public static bool DateIsToday(DateTime datetmp, DateTime dtCompareTo)
        {
            if (datetmp.Date.ToUniversalTime() == dtCompareTo.Date)
            {
                return true;
            }

            return false;
        }
    }
}
