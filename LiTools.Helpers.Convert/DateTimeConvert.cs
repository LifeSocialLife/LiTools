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
        /// Convert Datetime value to string. Only return date.
        /// </summary>
        /// <param name="dt">datetime value.</param>
        /// <returns>date as string.</returns>
        public static (bool Error, string DateAsString) DateToString(DateTime dt)
        {
            try
            {
                string tmpDate = dt.ToString("yyyMMdd");
                return (false, tmpDate);
            }
            catch
            {
                return (false, string.Empty);
            }
        }
    }
}
