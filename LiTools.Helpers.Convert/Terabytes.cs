using System;
using System.Collections.Generic;
using System.Text;

namespace LiTools.Helpers.Convert
{
    public static class Terabytes
    {
        public static double ToMegabytes(double terabytes) // BIGGER
        {
            // 1024 * 1024 megabytes in a terabyte
            return terabytes * (1024.0 * 1024.0);
        }

        public static double ToGigabytes(double terabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            return terabytes * 1024.0;
        }
    }
}
