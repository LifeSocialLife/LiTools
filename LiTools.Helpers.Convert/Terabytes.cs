// <summary>
// Webpage IO helper..
// </summary>
// <copyright file="Terabytes.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Convert Terabytes into ??.
    /// </summary>
    public static class Terabytes
    {
        /// <summary>
        /// Convert Terabytes to Megabytes.
        /// </summary>
        /// <param name="terabytes">terabytes.</param>
        /// <returns>Megabytes as double.</returns>
        public static double ToMegabytes(double terabytes) // BIGGER
        {
            // 1024 * 1024 megabytes in a terabyte
            return terabytes * (1024.0 * 1024.0);
        }

        /// <summary>
        /// Convert Terabytes to Gigabytes.
        /// </summary>
        /// <param name="terabytes">terabytes.</param>
        /// <returns>Gigabytes as double.</returns>
        public static double ToGigabytes(double terabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            return terabytes * 1024.0;
        }
    }
}
