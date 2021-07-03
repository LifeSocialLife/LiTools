// <copyright file="File.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// File helper.
    /// </summary>
    public static class File
    {
        public static byte[]? ReadBinaryFile(string filename)
        {
            try
            {
                return System.IO.File.ReadAllBytes(@filename);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Check if a file exist.
        /// </summary>
        /// <param name="file">file path</param>
        /// <returns>true or false.</returns>
        public static bool Exist(string file)
        {
            try
            {
                return System.IO.File.Exists(file);
            }
            catch
            {
                return false;
            }

        }
    }

   
}
