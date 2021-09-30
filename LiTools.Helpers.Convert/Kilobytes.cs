// <summary>
// Convert Kilobytes into.
// </summary>
// <copyright file="Kilobytes.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Convert Kilobytes into.
    /// </summary>
    public static class Kilobytes
    {
        /// <summary>
        /// To Megabytes.
        /// </summary>
        /// <param name="kilobytes">Kilobytes value.</param>
        /// <returns>Megabytes value as double.</returns>
        public static double ToMegabytes(long kilobytes)
        {
            return kilobytes / 1024f;
        }
    }
}
