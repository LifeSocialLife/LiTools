// <summary>
// Hardware helper.
// </summary>
// <copyright file="Hardware.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Get Harware information.
    /// </summary>
    public static class Hardware
    {
        /// <summary>
        /// Os types enums.
        /// </summary>
        public enum PlatformOsEnum
        {
            /// <summary>
            /// None. no value is set.
            /// </summary>
            None = 0,

            /// <summary>
            /// Unknown. Dont know what os this is.
            /// </summary>
            Unknown = 1,

            /// <summary>
            /// Windows.
            /// </summary>
            Windows = 3,

            /// <summary>
            /// Linux.
            /// </summary>
            Linux = 4,

            /// <summary>
            /// Osx.
            /// </summary>
            Osx = 5,
        }

        /// <summary>
        /// Architecture types.
        /// </summary>
        public enum ArchitectureEnum
        {
            /// <summary>
            /// Unknown.
            /// </summary>
            None = 0,

            /// <summary>
            /// X86 Architecture.
            /// </summary>
            X86 = 1,

            /// <summary>
            /// X64 Architecture.
            /// </summary>
            X64 = 2,

            /// <summary>
            /// Arm Architecture.
            /// </summary>
            Arm = 3,

            /// <summary>
            /// Arm64 Architecture.
            /// </summary>
            Arm64 = 4,
        }

        /// <summary>
        /// Get os this is running on. ex. windows, linux, osx.
        /// </summary>
        /// <returns>PlatformOsEnum.</returns>
        public static PlatformOsEnum GetOs()
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                return PlatformOsEnum.Windows;
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                return PlatformOsEnum.Linux;
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
            {
                return PlatformOsEnum.Osx;
            }
            else
            {
                return PlatformOsEnum.Unknown;
            }
        }

        /// <summary>
        /// Get Os Description.
        /// </summary>
        /// <returns>os description as string.</returns>
        public static string GetOsDescription()
        {
            return System.Runtime.InteropServices.RuntimeInformation.OSDescription;
        }

        /// <summary>
        /// Get Os Architecture from system.
        /// </summary>
        /// <returns>ArchitectureEnum.</returns>
        public static ArchitectureEnum GetOSArchitecture()
        {
            switch (System.Runtime.InteropServices.RuntimeInformation.OSArchitecture)
            {
                case System.Runtime.InteropServices.Architecture.X64:
                    return ArchitectureEnum.X64;

                case System.Runtime.InteropServices.Architecture.X86:
                    return ArchitectureEnum.X86;

                case System.Runtime.InteropServices.Architecture.Arm:
                    return ArchitectureEnum.Arm;

                case System.Runtime.InteropServices.Architecture.Arm64:
                    return ArchitectureEnum.Arm64;

                default:
                    return ArchitectureEnum.None;
            }
        }

        /// <summary>
        /// Get framwroek description.
        /// </summary>
        /// <returns>Net framwork version.</returns>
        public static string GetFrameworkDescription()
        {
            return System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
        }

        /// <summary>
        /// Get Processor Architecture.
        /// </summary>
        /// <returns>ArchitectureEnum.</returns>
        public static ArchitectureEnum GetProcessorArchitecture()
        {
            switch (System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture)
            {
                case System.Runtime.InteropServices.Architecture.X64:
                    return ArchitectureEnum.X64;

                case System.Runtime.InteropServices.Architecture.X86:
                    return ArchitectureEnum.X86;

                case System.Runtime.InteropServices.Architecture.Arm:
                    return ArchitectureEnum.Arm;

                case System.Runtime.InteropServices.Architecture.Arm64:
                    return ArchitectureEnum.Arm64;

                default:
                    return ArchitectureEnum.None;
            }
        }
    }
}
