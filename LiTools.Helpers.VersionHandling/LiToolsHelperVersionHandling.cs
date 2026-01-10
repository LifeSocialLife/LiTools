// <summary>
// LiSoLi - Project version setter.
// </summary>
// <copyright file="LiToolsHelperVersionHandling.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.VersionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Provides version and package information for YourProject assembly.
    /// </summary>
    public sealed class LiToolsHelperVersionHandling
    {
        private static readonly Assembly Assembly = typeof(LiToolsHelperVersionHandling).Assembly;

        /// <summary>
        /// Gets the package ID from the assembly (e.g., "YourProject.Name").
        /// </summary>
        public static string PackageId => AssemblyVersionHelper.GetPackageId(Assembly);

        /// <summary>
        /// Gets the package/informational version (e.g., "1.0.0-beta.1").
        /// This is the version from the &lt;Version&gt; property in .csproj.
        /// Use this static property to get version information without dependency injection.
        /// </summary>
        public static string Version => AssemblyVersionHelper.GetVersion(Assembly);

        /// <summary>
        /// Gets the assembly version (e.g., "1.0.0.0").
        /// </summary>
        public static System.Version? AssemblyVersion => Assembly.GetName().Version;

        /// <summary>
        /// Gets the file version.
        /// </summary>
        public static string? FileVersion =>
            Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;

        /// <summary>
        /// Gets the company name from the assembly.
        /// </summary>
        public static string? Company =>
            Assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;

        /// <summary>
        /// Gets the description from the assembly.
        /// </summary>
        public static string? Description =>
            Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

        /// <summary>
        /// Gets the build date from assembly metadata.
        /// </summary>
        public static string? BuildDate => AssemblyVersionHelper.GetMetadata(Assembly, "BuildDate");

        /// <summary>
        /// Gets the build site from assembly metadata.
        /// </summary>
        public static string? BuildSite => AssemblyVersionHelper.GetMetadata(Assembly, "BuildSite");

        /// <summary>
        /// Gets the build image from assembly metadata.
        /// </summary>
        public static string? BuildImage => AssemblyVersionHelper.GetMetadata(Assembly, "BuildImage");

        /// <summary>
        /// Gets a VersionModel instance populated with all version and build information.
        /// </summary>
        public static VersionModel GetVersionModel => AssemblyVersionHelper.CreateVersionModel(Assembly);

        /// <summary>
        /// Gets all assembly metadata as a dictionary.
        /// Use this static method to retrieve all version information without dependency injection.
        /// </summary>
        /// <returns>Dictionary containing all version and build metadata.</returns>
        public static IReadOnlyDictionary<string, string> GetAllMetadata() =>
            AssemblyVersionHelper.GetAllMetadata(Assembly);
    }
}

/*  Version history and information.
    - Version 1.0.0-alpha.1
      - Initial version.
*/
