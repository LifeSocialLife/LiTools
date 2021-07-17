
namespace LiTools.Helpers.Convert
{

    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Convert Megabytes into ??.
    /// </summary>
    public static class Megabytes
    {
        /// <summary>
        /// Convert to bytes.
        /// </summary>
        /// <param name="megabytes">megabytes.</param>
        /// <returns>bytes as double.</returns>
        public static double ToBytes(double megabytes)
        {
            return megabytes * 1024.0 * 1024.0;
        }

        /// <summary>
        /// Convert to kilobytes.
        /// </summary>
        /// <param name="megabytes">megabytes.</param>
        /// <returns>kilobytes as double.</returns>
        public static double ToKiloBytes(double megabytes)
        {
            return megabytes * 1024.0;
        }

        /// <summary>
        /// Convert megabytes to gigabytes.
        /// </summary>
        /// <param name="megabytes">megabytes.</param>
        /// <returns>Gigabytes as double.</returns>

        public static double ToGigabytes(double megabytes)
        {
            // 1024 megabyte in a gigabyte
            return megabytes / 1024.0;
        }

        /// <summary>
        /// Convert megabytes to terabytes.
        /// </summary>
        /// <param name="megabytes">megabytes.</param>
        /// <returns>Gigabytes as double.</returns>
        public static double ToTerabytes(double megabytes)
        {
            // 1024 * 1024 megabytes in a terabyte
            return megabytes / (1024.0 * 1024.0);
        }

    }
}
