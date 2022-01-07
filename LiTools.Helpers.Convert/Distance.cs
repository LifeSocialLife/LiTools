// <summary>
// Length, Height and Distance.
// </summary>
// <copyright file="Distance.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

// https://www.nottingham.ac.uk/manuscriptsandspecialcollections/researchguidance/weightsandmeasures/measurements.aspx
// https://coolconversion.com/lenght/meter-to-barleycorn
// .
// .
namespace LiTools.Helpers.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Distance.
    /// </summary>
    public static class Distance
    {
        /// <summary>
        /// Meter Convert into?.
        /// </summary>
        /// <param name="to">Convert into.</param>
        /// <param name="value">Meter Value as double.</param>
        /// <returns>Selected value as double.</returns>
        public static double Meter(DistanceEnums to, double value)
        {
            return to switch
            {
                DistanceEnums.Angstrom => value * 10000000000,
                DistanceEnums.Bamboo => value * 0.3125,
                DistanceEnums.Barleycorn => value * 117.647058824,
                DistanceEnums.Centimeter => value * 100,
                DistanceEnums.Fathom => value * 0.546806649169,
                DistanceEnums.Finger => value * 8.7489063867,
                DistanceEnums.Foot => value * 3.28083989501,
                DistanceEnums.Hand => value * 9.84251968504,
                DistanceEnums.Inch => value * 39.3700787402,
                DistanceEnums.Kilometer => value / 0.001,
                DistanceEnums.Meter => value,
                DistanceEnums.Mile => value * 0.000621371192,
                DistanceEnums.Millimeter => value * 1000,
                DistanceEnums.Yard => value * 1.09361329834,
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// Convert meter per second into knots.
        /// </summary>
        /// <param name="ms">meter per second.</param>
        /// <returns>knots as double.</returns>
        public static double Meter_PerSecond_To_Knots(double ms)
        {
            return ms * 1.94384449;
        }
    }
}
