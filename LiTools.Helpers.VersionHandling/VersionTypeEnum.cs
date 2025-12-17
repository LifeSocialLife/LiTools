// <summary>
// LiSoLi - Version type enum.
// </summary>
// <copyright file="VersionTypeEnum.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.VersionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Version release type.
    /// </summary>
    public enum VersionTypeEnum
    {
        /// <summary>Alpha version.</summary>
        Alpha = 0,

        /// <summary>Beta version.</summary>
        Beta = 1,

        /// <summary>Pre-release version (release candidate).</summary>
        ReleaseCandidate = 2,

        /// <summary>Release version.</summary>
        Release = 3,
    }
}