// <summary>
// Enum.
// </summary>
// <copyright file="Enums.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// Filesize types in bytes.
    /// </summary>
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

    /// <summary>
    /// Measurments Lrngth, Hright and Distance.
    /// </summary>
    public enum DistanceEnums
    {
        /// <summary>Kilometer.</summary>
        Kilometer = 1,

        /// <summary>Meter.</summary>
        Meter = 2,

        /// <summary>Centimeter.</summary>
        Centimeter = 3,

        /// <summary>Millimeter.</summary>
        Millimeter = 4,

        /// <summary>Angstrom.</summary>
        Angstrom = 5,

        /// <summary>Mile.</summary>
        Mile = 6,

        /// <summary>Fathom.</summary>
        Fathom = 7,

        /// <summary>Yard.</summary>
        Yard = 8,

        /// <summary>Foot.</summary>
        Foot = 9,

        /// <summary>Hand.</summary>
        Hand = 10,

        /// <summary>Inch.</summary>
        Inch = 11,

        /// <summary>Finger.</summary>
        Finger = 12,

        /// <summary>Bamboo.</summary>
        Bamboo = 13,

        /// <summary>3 barleycorns = 1 inch.</summary>
        Barleycorn = 14,
    }
}
