// <summary>
// LiSoLi - Version model.
// </summary>
// <copyright file="VersionModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.VersionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Version information model.
    /// </summary>
    public sealed class VersionModel
    {
        /// <summary>
        /// Gets or sets Software name.
        /// </summary>
        /// <remarks>JSON name: 'vsoftware'.</remarks>
        [JsonPropertyName("vsoftware")]
        public string SoftwareName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets version major.
        /// </summary>
        /// <remarks>JSON name: 'vmajor'.</remarks>
        [JsonPropertyName("vmajor")]
        public uint VersionMajor { get; set; } = 0;

        /// <summary>
        /// Gets or sets version minor.
        /// </summary>
        /// <remarks>JSON name: 'vminor'.</remarks>
        [JsonPropertyName("vminor")]
        public uint VersionMinor { get; set; } = 0;

        /// <summary>
        /// Gets or sets version patch.
        /// </summary>
        /// <remarks>JSON name: 'vpatch'.</remarks>
        [JsonPropertyName("vpatch")]
        public uint VersionPatch { get; set; } = 0;

        /// <summary>
        /// Gets or sets release type.
        /// </summary>
        /// <remarks>JSON name: 'rtype'.</remarks>
        [JsonPropertyName("rtype")]
        public VersionTypeEnum ReleaseType { get; set; } = VersionTypeEnum.Release;

        /// <summary>
        /// Gets or sets extra version number used with pre-release types (e.g., alpha.1, beta.2, rc.3).
        /// </summary>
        /// <remarks>JSON name: 'vextra'.</remarks>
        [JsonPropertyName("vextra")]
        public uint VersionExtra { get; set; } = 0;

        /// <summary>
        /// Gets or sets the build date as a string (e.g., ISO 8601 format: 2025-10-15T12:34:56Z).
        /// </summary>
        /// <remarks>JSON name: 'bdate'.</remarks>
        [JsonPropertyName("bdate")]
        public string BuildDate { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the build site or environment identifier (e.g., CI/CD agent name or region).
        /// </summary>
        /// <remarks>JSON name: 'bsite'.</remarks>
        [JsonPropertyName("bsite")]
        public string BuildSite { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the build image identifier (e.g., container image tag or artifact id).
        /// </summary>
        /// <remarks>JSON name: 'bimage'.</remarks>
        [JsonPropertyName("bimage")]
        public string BuildImage { get; set; } = string.Empty;

        /// <summary>
        /// Gets the composed semantic version string.
        /// Pattern: major.minor.patch[-alpha|-beta|-rc][.extra].
        /// Example: 1.2.3-rc.2.
        /// </summary>
        /// <remarks>JSON name: 'vstring'.</remarks>
        [JsonPropertyName("vstring")]
        public string VersionString
        {
            get
            {
                string versionString = $"{this.VersionMajor}.{this.VersionMinor}.{this.VersionPatch}";

                switch (this.ReleaseType)
                {
                    case VersionTypeEnum.Alpha:
                        versionString += "-alpha";
                        break;
                    case VersionTypeEnum.Beta:
                        versionString += "-beta";
                        break;
                    case VersionTypeEnum.ReleaseCandidate:
                        versionString += "-rc";
                        break;
                    case VersionTypeEnum.Release:
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown release type: {this.ReleaseType}");
                }

                if (this.ReleaseType != VersionTypeEnum.Release && this.VersionExtra > 0)
                {
                    versionString += $".{this.VersionExtra}";
                }

                return versionString;
            }
        }

        /// <summary>
        /// Save this model as a json string.
        /// </summary>
        /// <returns>json as string.</returns>
        public string ToJsonString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Creates a shallow copy of this model.
        /// All properties are value types or immutable strings, so this is effectively a deep copy.
        /// </summary>
        /// <returns>A new instance with copied property values.</returns>
        public VersionModel Clone()
        {
            return new VersionModel
            {
                SoftwareName = this.SoftwareName,
                VersionMajor = this.VersionMajor,
                VersionMinor = this.VersionMinor,
                VersionPatch = this.VersionPatch,
                ReleaseType = this.ReleaseType,
                VersionExtra = this.VersionExtra,
                BuildDate = this.BuildDate,
                BuildSite = this.BuildSite,
                BuildImage = this.BuildImage,
            };
        }
    }
}