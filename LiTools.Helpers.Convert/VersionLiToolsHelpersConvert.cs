// <summary>
// LiSoLi - Project version setter.
// </summary>
// <copyright file="VersionLiToolsHelpersConvert.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Organize
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using LiTools.Helpers.VersionHandling;

    /// <summary>
    /// Project version setter.
    /// </summary>
    /// <param name="version">VersionService.</param>
    public sealed class VersionLiToolsHelpersConvert(VersionService version)
    {
        private readonly string name = "LiToolsHelpersConvert".ToLower();

        /// <summary>
        /// Gets get name of this software.
        /// </summary>
        public string GetName => this.name;

        /// <summary>
        /// Set version of this build.
        /// </summary>
        public void SetVersion()
        {
            version.VersionUpdate(v =>
            {
                v.SoftwareName = this.name;
                v.VersionMajor = 1;
                v.VersionMinor = 0;
                v.VersionPatch = 0;
                v.ReleaseType = VersionTypeEnum.Alpha;
                v.VersionExtra = 1;
                v.BuildDate = VersionData.BuildInfo.BuildDate;
                v.BuildSite = VersionData.BuildInfo.BuildSite;
                v.BuildImage = VersionData.BuildInfo.BuildImage;
            });
        }
    }
}

/*  Version history and information.
    - Version 1.0.0-alpha.1
      - Initial version.
*/