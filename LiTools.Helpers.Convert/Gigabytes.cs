// <copyright file="Gigabytes.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>

namespace LiTools.Helpers.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Convert Gigabytes into.
    /// </summary>
    public static class Gigabytes
    {
        /// <summary>
        /// To Terabytes.
        /// </summary>
        /// <param name="gigabytes">Gigabytes value.</param>
        /// <returns>Terabytes value as double.</returns>
        public static double ToTerabytes(double gigabytes) // SMALLER
        {
            // 1024 gigabytes in a terabyte
            return gigabytes / 1024.0;
        }

        /// <summary>
        /// Into Megabytes.
        /// </summary>
        /// <param name="gigabytes">Input Gigabytes as double.</param>
        /// <returns>Megabytes value as double.</returns>
        public static double ToMegabytes(double gigabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            return gigabytes * 1024.0;
        }
    }
}
