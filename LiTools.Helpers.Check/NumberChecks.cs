// <summary>
// NumberChecks.
// </summary>
// <copyright file="NumberChecks.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Check
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Number checks.
    /// </summary>
    public static class NumberChecks
    {
        /// <summary>
        /// Check if a number is power of x value.
        /// </summary>
        /// <param name="value">Value that shod be checked.</param>
        /// <param name="x">Power of value.</param>
        /// <returns>true or false.</returns>
        public static bool PowerOf(int value, int x)
        {
            while (true)
            {
                value = value - x;
                if (value == 0)
                {
                    return true;
                }
                else if (value < 0)
                {
                    return false;
                }
            }
        }
    }
}
