// <summary>
// LiSoLi - version service- Assembly Version helper.
// </summary>
// <copyright file="AssemblyVersionHelper.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.VersionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Helper class for extracting version information from assemblies.
    /// </summary>
    public static class AssemblyVersionHelper
    {
        /// <summary>
        /// Gets the package ID from an assembly.
        /// </summary>
        /// <param name="assembly">The assembly to extract package ID from.</param>
        /// <returns>The package ID, assembly name, or "unknown" if not found.</returns>
        public static string GetPackageId(Assembly assembly) =>
            assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product
            ?? assembly.GetName().Name
            ?? "unknown";

        /// <summary>
        /// Gets the version string from an assembly.
        /// </summary>
        /// <param name="assembly">The assembly to extract version from.</param>
        /// <returns>The informational version, assembly version, or "unknown" if not found.</returns>
        public static string GetVersion(Assembly assembly) =>
            assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
            ?? assembly.GetName().Version?.ToString()
            ?? "unknown";

        /// <summary>
        /// Gets assembly metadata by key.
        /// </summary>
        /// <param name="assembly">The assembly to extract metadata from.</param>
        /// <param name="key">The metadata key to retrieve.</param>
        /// <returns>The metadata value if found, otherwise null.</returns>
        public static string? GetMetadata(Assembly assembly, string key) =>
            assembly.GetCustomAttributes<AssemblyMetadataAttribute>()
                .FirstOrDefault(attr => attr.Key == key)?.Value;

        /// <summary>
        /// Creates a VersionModel from an assembly.
        /// </summary>
        /// <param name="assembly">The assembly to create version model from.</param>
        /// <returns>A populated VersionModel instance with version and build information.</returns>
        public static VersionModel CreateVersionModel(Assembly assembly)
        {
            var packageId = GetPackageId(assembly);
            var version = GetVersion(assembly);
            var (major, minor, patch, releaseType, extra) = ParseVersionString(version);

            return new VersionModel
            {
                SoftwareName = packageId?.ToLower() ?? "unknown",
                VersionMajor = major,
                VersionMinor = minor,
                VersionPatch = patch,
                ReleaseType = releaseType,
                VersionExtra = extra,
                BuildDate = GetMetadata(assembly, "BuildDate") ?? "missing",
                BuildSite = GetMetadata(assembly, "BuildSite") ?? "missing",
                BuildImage = GetMetadata(assembly, "BuildImage") ?? "missing",
            };
        }

        /// <summary>
        /// Gets all metadata as a dictionary.
        /// </summary>
        /// <param name="assembly">The assembly to extract all metadata from.</param>
        /// <returns>A read-only dictionary containing all version and build metadata.</returns>
        public static IReadOnlyDictionary<string, string> GetAllMetadata(Assembly assembly)
        {
            var metadata = new Dictionary<string, string>
            {
                ["PackageId"] = GetPackageId(assembly),
                ["Version"] = GetVersion(assembly),
                ["AssemblyVersion"] = assembly.GetName().Version?.ToString() ?? "unknown",
                ["FileVersion"] = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ?? "unknown",
                ["Company"] = assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? "unknown",
                ["Description"] = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? "unknown",
            };

            foreach (var attr in assembly.GetCustomAttributes<AssemblyMetadataAttribute>())
            {
                metadata[attr.Key] = attr.Value ?? string.Empty;
            }

            return metadata;
        }

        /// <summary>
        /// Parses a semantic version string into its components.
        /// </summary>
        /// <param name="versionString">The semantic version string to parse (e.g., "1.2.3-beta.4").</param>
        /// <returns>A tuple containing major, minor, patch, release type, and extra version number.</returns>
        private static (uint Major, uint Minor, uint Patch, VersionTypeEnum ReleaseType, uint Extra) ParseVersionString(string versionString)
        {
            uint major = 0, minor = 0, patch = 0, extra = 0;
            var releaseType = VersionTypeEnum.Release;

            if (string.IsNullOrWhiteSpace(versionString) || versionString == "unknown")
            {
                return (major, minor, patch, releaseType, extra);
            }

            var match = Regex.Match(versionString, @"^(\d+)\.(\d+)\.(\d+)(?:-(\w+)(?:\.(\d+))?)?");

            if (match.Success)
            {
                major = uint.TryParse(match.Groups[1].Value, out var maj) ? maj : 0;
                minor = uint.TryParse(match.Groups[2].Value, out var min) ? min : 0;
                patch = uint.TryParse(match.Groups[3].Value, out var pat) ? pat : 0;

                if (match.Groups[4].Success)
                {
                    var releaseTypeStr = match.Groups[4].Value.ToLowerInvariant();
                    releaseType = releaseTypeStr switch
                    {
                        "alpha" => VersionTypeEnum.Alpha,
                        "beta" => VersionTypeEnum.Beta,
                        "rc" => VersionTypeEnum.ReleaseCandidate,
                        _ => VersionTypeEnum.Release,
                    };

                    if (match.Groups[5].Success)
                    {
                        extra = uint.TryParse(match.Groups[5].Value, out var ext) ? ext : 0;
                    }
                }
            }

            return (major, minor, patch, releaseType, extra);
        }
    }
}
