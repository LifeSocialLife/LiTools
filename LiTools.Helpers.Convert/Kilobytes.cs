using System;
using System.Collections.Generic;
using System.Text;

namespace LiTools.Helpers.Convert
{
    public static class Kilobytes
    {
        public static double ToMegabytes(long kilobytes)
        {
            return kilobytes / 1024f;
        }
    }
}
