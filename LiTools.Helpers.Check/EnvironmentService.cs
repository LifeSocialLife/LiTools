// <summary>
// Environment service.
// </summary>
// <copyright file="EnvironmentService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>

namespace LiTools.Helpers.Check
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Environment service class.
    /// </summary>
    public class EnvironmentService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentService"/> class.
        /// </summary>
        public EnvironmentService()
        {
            if (!this.Collected)
            {
                this.CollectData();
            }
        }

        /// <summary>
        /// Gets a value indicating whether error collecting information.
        /// </summary>
        public bool HasErrors { get; private set; } = false;

        private bool Collected { get; set; } = false;

        private EnvironmentInfoModel EnvironmentInfo { get; set; } = new();

        /// <summary>
        /// Get the model.
        /// </summary>
        /// <returns>EnvironmentInfoModel.</returns>
        public EnvironmentInfoModel GetModel()
        {
            if (!this.Collected)
            {
                this.CollectData();
            }

            return this.EnvironmentInfo;
        }

        private void CollectData()
        {
            try
            {
                // Get information about the operating system
                this.EnvironmentInfo.OSVersion = Environment.OSVersion.VersionString;
                this.EnvironmentInfo.Platform = Environment.OSVersion.Platform.ToString();
                this.EnvironmentInfo.ServicePack = Environment.OSVersion.ServicePack;
                this.EnvironmentInfo.VersionString = Environment.OSVersion.VersionString;

                // Get information about the current user
                this.EnvironmentInfo.UserName = Environment.UserName;
                this.EnvironmentInfo.UserDomainName = Environment.UserDomainName;

                // Get information about the machine
                this.EnvironmentInfo.MachineName = Environment.MachineName;
                this.EnvironmentInfo.ProcessorCount = Environment.ProcessorCount;
                this.EnvironmentInfo.SystemDirectory = Environment.SystemDirectory;
                this.EnvironmentInfo.SystemPageSize = Environment.SystemPageSize;

                // Get information about the current process
                this.EnvironmentInfo.ProcessId = Process.GetCurrentProcess().Id;
                this.EnvironmentInfo.ProcessorAffinity = Process.GetCurrentProcess().ProcessorAffinity.ToString();
                this.EnvironmentInfo.WorkingSet = Environment.WorkingSet;
                this.EnvironmentInfo.ApplicationCommandArgs = Environment.GetCommandLineArgs();

                // Get information about the application domain (folders).
                this.EnvironmentInfo.ApplicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                this.EnvironmentInfo.ApplicationStartingDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);

                // Get the path to the local AppData folder
                this.EnvironmentInfo.LocalAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // Get all environment variables
                if (Environment.GetEnvironmentVariables() is IDictionary<string, string> envVars)
                {
                    this.EnvironmentInfo.EnvironmentVariables = envVars;
                }

                /* Old code. remove later when we know new code is working
                IDictionary<string, string>? envVars = Environment.GetEnvironmentVariables() as IDictionary<string, string>;
                if (envVars != null)
                {
                    this.EnvironmentInfo.EnvironmentVariables = envVars;
                }
                */
                this.HasErrors = false;
            }
            catch
            {
                this.HasErrors = true;
            }

            this.Collected = true;
        }

        /// <summary>
        /// Environment information model.
        /// </summary>
        public class EnvironmentInfoModel
        {
            /// <summary>
            /// Gets or sets os version.
            /// </summary>
            public string OSVersion { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets platsform we are running on.
            /// </summary>
            public string Platform { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets service pack if any.
            /// </summary>
            public string ServicePack { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets version.
            /// </summary>
            public string VersionString { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets username.
            /// </summary>
            public string UserName { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets domain user name.
            /// </summary>
            public string UserDomainName { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets machine name.
            /// </summary>
            public string MachineName { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets processor count.
            /// </summary>
            public int ProcessorCount { get; set; } = 0;

            /// <summary>
            /// Gets or sets system directory.
            /// </summary>
            public string SystemDirectory { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets system page size.
            /// </summary>            public string SystemDirectory { get; set; } = string.Empty;
            public int SystemPageSize { get; set; }

            /// <summary>
            /// Gets or sets process id.
            /// </summary>
            public int ProcessId { get; set; }

            /// <summary>
            /// Gets or sets processor affinity.
            /// </summary>
            public string ProcessorAffinity { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets working set.
            /// </summary>
            public long WorkingSet { get; set; }

            /// <summary>
            /// Gets or sets where is the application stored.
            /// </summary>
            public string ApplicationBaseDirectory { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets from folder where application is started.
            /// </summary>
            public string ApplicationStartingDirectory { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets starting command whit args.
            /// </summary>
            public string[] ApplicationCommandArgs { get; set; } = Array.Empty<string>();

            /// <summary>
            /// Gets or sets local app data folder.
            /// </summary>
            public string LocalAppDataFolder { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets environment variables.
            /// </summary>
            public IDictionary<string, string> EnvironmentVariables { get; set; } = new Dictionary<string, string>();
        }
    }
}
