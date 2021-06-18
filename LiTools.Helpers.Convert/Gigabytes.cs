using System;
using System.Collections.Generic;
using System.Text;

namespace LiTools.Helpers.Convert
{
    public static class Gigabytes
    {
        public static double ToMegabytes(double gigabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            return gigabytes * 1024.0;
        }

        public static double ToTerabytes(double gigabytes) // SMALLER
        {
            // 1024 gigabytes in a terabyte
            return gigabytes / 1024.0;
        }

    }
}
