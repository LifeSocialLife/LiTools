// <summary>
// BitsChecks.
// </summary>
// <copyright file="BitsChecks.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Check
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Bits Checks.
    /// </summary>
    public static class BitsChecks
    {
        /// <summary>
        /// Check if int is power of 8.
        /// </summary>
        /// <param name="n">bits count.</param>
        /// <returns>true or false.</returns>
        public static bool CheckPowerof8(int n)
        {
            // calculate log8(n) */
            double i = Math.Log(n) / Math.Log(8);

            // check if i is an integer or not */
            return i - Math.Floor(i) < 0.000001;
        }
    }
}
