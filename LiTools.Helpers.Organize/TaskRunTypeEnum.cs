// <summary>
// TaskRunTypeEnum.
// </summary>
// <copyright file="TaskRunTypeEnum.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Organize
{
    /// <summary>
    /// Task work types. short running or long running background work.
    /// </summary>
    public enum TaskRunTypeEnum
    {
        /// <summary>only do define.</summary>
        None,

        /// <summary>This is a short running work.</summary>
        Short,

        /// <summary>This is a long running work.</summary>
        Long,

        /// <summary>This shod only be run once.</summary>
        OnlyOnce,
    }
}
