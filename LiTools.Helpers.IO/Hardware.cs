using System;
using System.Collections.Generic;
using System.Text;

namespace LiTools.Helpers.IO
{
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

        public enum ArchitectureEnum
        {
            none = 0,
            X86 = 1,
            X64 = 2,
            Arm = 3,
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

        public static string GetOsDescription()
        {
            return System.Runtime.InteropServices.RuntimeInformation.OSDescription;
        }

        /// <summary>
        /// Get Os Architecture from system.
        /// </summary>
        /// <returns></returns>
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
                    return ArchitectureEnum.none;

            }
        }

        public static string GetFrameworkDescription()
        {
            return System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
        }

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
                    return ArchitectureEnum.none;
            }
        }
    }
}
