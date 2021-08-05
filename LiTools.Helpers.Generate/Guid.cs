// <summary>
// Generate GUID.
// </summary>
// <copyright file="Guid.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

// https://docs.microsoft.com/en-us/dotnet/api/system.guid.tostring?view=net-5.0
// -.
namespace LiTools.Helpers.Generate.Dev
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Generate GUID.
    /// </summary>
    public static class Guid
    {
        /// <summary>
        /// 32 digits.
        /// </summary>
        /// <returns>32 digits ex: 00000000000000000000000000000000 .</returns>
        public static string AsString()
        {
            return System.Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 32 digits separated by hyphens.
        /// </summary>
        /// <returns>32 digits separated by hyphens. ex: 00000000-0000-0000-0000-000000000000 .</returns>
        public static string AsStringSeperatedByHyphens()
        {
            return System.Guid.NewGuid().ToString("D");
        }

        /// <summary>
        /// A new GUID object.
        /// </summary>
        /// <returns>New GUID as type GUID.</returns>
        public static System.Guid New()
        {
            return System.Guid.NewGuid();
        }
    }
}
