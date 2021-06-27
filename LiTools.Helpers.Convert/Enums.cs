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
}
