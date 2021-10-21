// <copyright file="Enums.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>

namespace LiTools.Helpers.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// Filesize types in bytes.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileNameMustMatchTypeName", Justification = "Reviewed.")]
    public enum FileSizeEnums
    {
        /// <summary>Bytes.</summary>
        Bytes = 0,

        /// <summary>Kilobytes.</summary>
        Kilobytes = 1,

        /// <summary>Megabytes.</summary>
        Megabytes = 2,

        /// <summary>Gigabytes.</summary>
        Gigabytes = 3,
    }

    // https://www.nottingham.ac.uk/manuscriptsandspecialcollections/researchguidance/weightsandmeasures/measurements.aspx

    public enum MeasurementsEnums
    {
        /// <summary>3 barleycorns	1 inch.</summary>
        Barleycorns = 1,
        /// <summary>Gigabytes.</summary>
        Inches = 2,
        /// <summary>Gigabytes.</summary>
        Yards = 3,
        /// <summary>Gigabytes.</summary>
        Poles = 4,
        /// <summary>Gigabytes.</summary>
        Furlongs = 5,
        /// <summary>Gigabytes.</summary>
        Miles = 6,



    }

    /* for multi line comments
     * /// <summary>Gigabytes.</summary>
  (in or ")
12 inches	1 foot (ft or ')
3 feet	1 yard (yd)
5½ yards	1 perch, pole or rod
40 poles	1 furlong
8 furlongs	1 mile
3 miles	1 league

 */

}
