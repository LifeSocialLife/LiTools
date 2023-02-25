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
            if (!this.Collected) {
                this.CollectData();
            }
        }

        private bool Collected { get; set; } = false;

        private EnvironmentInfoModel EnvironmentInfo { get; set; } = new();

        /// <summary>
        /// Error collecting information.
        /// </summary>
        public bool HasErrors { get; private set; } = false;

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

                // Get information about the application domain
                this.EnvironmentInfo.ApplicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Get the path to the local AppData folder
                this.EnvironmentInfo.LocalAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // Get all environment variables
                IDictionary<string, string> envVars = Environment.GetEnvironmentVariables() as IDictionary<string, string>;
                this.EnvironmentInfo.EnvironmentVariables = envVars;

                this.HasErrors = false;
            }
            catch
            {
                this.HasErrors = true;
            }

            this.Collected = true;
        }

        public class EnvironmentInfoModel
        {
            public string OSVersion { get; set; }
            public string Platform { get; set; }
            public string ServicePack { get; set; }
            public string VersionString { get; set; }
            public string UserName { get; set; }
            public string UserDomainName { get; set; }
            public string MachineName { get; set; }
            public int ProcessorCount { get; set; }
            public string SystemDirectory { get; set; }
            public int SystemPageSize { get; set; }
            public int ProcessId { get; set; }
            public string ProcessorAffinity { get; set; }
            public long WorkingSet { get; set; }
            public string ApplicationBaseDirectory { get; set; }
            public string LocalAppDataFolder { get; set; }
            public IDictionary<string, string> EnvironmentVariables { get; set; }
        }
    }
}
