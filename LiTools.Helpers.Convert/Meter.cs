// <summary>
// Convert Megabytes into.
// </summary>
// <copyright file="Meter.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

// https://www.nottingham.ac.uk/manuscriptsandspecialcollections/researchguidance/weightsandmeasures/measurements.aspx
// .
namespace LiTools.Helpers.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Convert Meter into ??.
    /// </summary>
    public static class Meter
    {
        /// <summary>
        /// Convert meter per second into knots.
        /// </summary>
        /// <param name="ms">meter per second.</param>
        /// <returns>knots as double.</returns>
        public static double PerSecond_To_Knots(double ms)
        {
            return ms * 1.94384449;
        }

        /// <summary>
        /// Meter to mile.
        /// </summary>
        /// <param name="meter">meter as double.</param>
        /// <returns>Mile as double.</returns>
        public static double To_Mile(double meter)
        {
            return meter * 0.000621371192;
        }
    }
}
