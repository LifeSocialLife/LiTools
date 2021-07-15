// <summary>
// Drives information.
// </summary>
// <copyright file="Drives.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Drives helper.
    /// </summary>
    public static class Drives
    {
        /// <summary>
        /// Get informaton of all drives in the system.
        /// </summary>
        /// <param name="getOnlyFixedDrives">Show only fixed drives.</param>
        /// <param name="getOnlyReadyDrives">Show only ready drives.</param>
        /// <returns>GetDrivesInformationReturnModel as list.</returns>
        public static List<GetDrivesInformationReturnModel> GetDrivesInformation(bool getOnlyFixedDrives = false, bool getOnlyReadyDrives = true)
        {
            var tmpReturn = new List<GetDrivesInformationReturnModel>();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (getOnlyReadyDrives && !drive.IsReady)
                {
                    // This drive is not ready. and we only want ready drives.
                    continue;
                }

                if (getOnlyFixedDrives && drive.DriveType != DriveType.Fixed)
                {
                    // This drive is not fixed. and we only want fixed drives.
                    continue;
                }

                var aa = new GetDrivesInformationReturnModel()
                {
                    AvailableFreeSpace = drive.AvailableFreeSpace,
                    DriveFormat = drive.DriveFormat,
                    DriveType = drive.DriveType,
                    VolumeLabel = drive.VolumeLabel,
                    TotalSize = drive.TotalSize,
                    TotalFreeSpace = drive.TotalFreeSpace,
                    Name = drive.Name,
                    IsReady = drive.IsReady,
                };

                tmpReturn.Add(aa);
            }

            return tmpReturn;
        }

        /// <summary>
        /// GetDrivesInformation Return model.
        /// </summary>
        public class GetDrivesInformationReturnModel
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="GetDrivesInformationReturnModel"/> class.
            /// </summary>
            public GetDrivesInformationReturnModel()
            {
                this.AvailableFreeSpace = 0;
                this.DriveFormat = string.Empty;
                this.DriveType = DriveType.Unknown;
                this.IsReady = false;
                this.Name = string.Empty;
                this.TotalFreeSpace = 0;
                this.TotalSize = 0;
                this.VolumeLabel = string.Empty;
            }

            /// <summary>
            /// Gets or sets. Indicates the amount of available free space on a drive, in bytes.
            /// </summary>
            public long AvailableFreeSpace { get; set; }

            /// <summary>
            /// Gets or sets. Gets the name of the file system, such as NTFS or FAT32.
            /// </summary>
            public string DriveFormat { get; set; }

            /// <summary>
            /// Gets or sets the drive type, such as CD-ROM, removable, network, or fixed.
            /// </summary>
            public DriveType DriveType { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether gets a value that indicates whether a drive is ready.
            /// </summary>
            public bool IsReady { get; set; }

            /// <summary>
            /// Gets or sets the name of a drive, such as C:\.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the total size of storage space on a drive, in bytes.
            /// </summary>
            public long TotalFreeSpace { get; set; }

            /// <summary>
            /// Gets or sets the total size of storage space on a drive, in bytes.
            /// </summary>
            public long TotalSize { get; set; }

            /// <summary>
            /// Gets or sets the volume label of a driv.
            /// </summary>
            public string VolumeLabel { get; set; }
        }
    }
}
