// <copyright file="Bytes.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>

namespace LiTools.Helpers.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Convert Bytes into ?.
    /// </summary>
    public static class Bytes
    {
        /// <summary>
        /// Automatik convert into Gigabytes, Megabytes, Kilobyts.
        /// </summary>
        /// <param name="bytes">input value in bytes that shod be converted.</param>
        /// <returns>Value returnd as selected return type.</returns>
        public static Tuple<FileSizeEnums, double> To(long bytes)
        {
            if (bytes == 0)
            {
                return new Tuple<FileSizeEnums, double>(FileSizeEnums.Bytes, 0);
            }
            else if (bytes >= 1073741824)
            {
                // Gigabytes
                return new Tuple<FileSizeEnums, double>(FileSizeEnums.Gigabytes, ToGigabytes(bytes));
            }
            else if (bytes >= 1048576)
            {
                // Megabytes
                return new Tuple<FileSizeEnums, double>(FileSizeEnums.Megabytes, ToMegabytes(bytes));
            }
            else if (bytes >= 1024)
            {
                // Kilobytes
                return new Tuple<FileSizeEnums, double>(FileSizeEnums.Kilobytes, ToKilobytes(bytes));
            }

            return new Tuple<FileSizeEnums, double>(FileSizeEnums.Bytes, bytes);
        }

        /// <summary>
        /// Convert bytes into ?.
        /// </summary>
        /// <param name="data">Bytes, Kilobytes, Megabytes, Gigabytes.</param>
        /// <param name="bytes">Input value in bytes.</param>
        /// <returns>Value returnd as data return type.</returns>
        public static double To(FileSizeEnums data, long bytes)
        {
            switch (data)
            {
                case FileSizeEnums.Bytes:
                    {
                        return bytes;
                    }

                case FileSizeEnums.Gigabytes:
                    return ToGigabytes(bytes);

                case FileSizeEnums.Kilobytes:
                    return ToKilobytes(bytes);

                case FileSizeEnums.Megabytes:
                    return ToMegabytes(bytes);

                default:
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

                    throw new NotImplementedException();
            }
        }

        private static double ToKilobytes(long bytes)
        {
            return bytes / 1024f;
        }

        private static double ToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        private static double ToGigabytes(long bytes)
        {
            return ToMegabytes(bytes) / 1024f;
        }
    }
}
