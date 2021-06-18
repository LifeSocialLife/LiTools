using System;
using System.Collections.Generic;
using System.Text;

namespace LiTools.Helpers.Convert
{
    public static class Megabytes
    {
        public static double ToGigabytes(double megabytes) 
        {
            // 1024 megabyte in a gigabyte
            return megabytes / 1024.0;
        }

        public static double ToTerabytes(double megabytes) 
        {
            // 1024 * 1024 megabytes in a terabyte
            return megabytes / (1024.0 * 1024.0);
        }

    }
}
