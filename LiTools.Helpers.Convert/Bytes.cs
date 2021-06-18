using System;
using System.Collections.Generic;
using System.Text;

namespace LiTools.Helpers.Convert
{
    /// <summary>
    /// Convert Bytes into ? 
    /// </summary>
    public static class Bytes
    {
        public static double To(FileSizeEnums data, long bytes)
        {
            switch (data)
            {
                case FileSizeEnums.Bytes:
                    {
                        return bytes;
                    }
                case FileSizeEnums.Kilobytes:
                    return ToKilobytes(bytes);

                default:
                    break;
            }

            return 0;
        }
        public static double ToKilobytes(long bytes)
        {
            return bytes / 1024f;
        }
        public static double ToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public static double ToGigabytes(long bytes)
        {
            return ToMegabytes(bytes) / 1024f;
        }
    }
}
