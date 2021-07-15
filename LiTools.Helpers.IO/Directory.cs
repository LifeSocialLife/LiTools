// <summary>
// Starter no Gui Worker class.
// </summary>
// <copyright file="Directory.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Directory helper class. System.IO .
    /// </summary>
    public static class Directory
    {
        /// <summary>
        /// Get all directories from a directory.
        /// </summary>
        /// <param name="directory">folder to scan.</param>
        /// <param name="recursive">look inside child folders.</param>
        /// <returns>true if no catch.</returns>
        public static Tuple<bool, List<string>> GetSubdirectoryList(string directory, bool recursive)
        {
            try
            {
                // string[] folders;
                List<string> folderList = new List<string>();

                if (recursive)
                {
                    folderList = System.IO.Directory.GetDirectories(@directory, "*", System.IO.SearchOption.AllDirectories).ToList();

                    // folders = System.IO.Directory.GetDirectories(@directory, "*", System.IO.SearchOption.AllDirectories);
                }
                else
                {
                    folderList = System.IO.Directory.GetDirectories(@directory, "*", System.IO.SearchOption.TopDirectoryOnly).ToList();

                    // folders = System.IO.Directory.GetDirectories(@directory, "*", System.IO.SearchOption.TopDirectoryOnly);
                }

                // foreach (string folder in folders)
                // {
                //    folderList.Add(folder);
                // }
                return new Tuple<bool, List<string>>(true, folderList);
            }
            catch (Exception)
            {
                return new Tuple<bool, List<string>>(false, new List<string>());
            }
        }

        /// <summary>
        /// Delete directory.
        /// </summary>
        /// <param name="dir">directory to delete.</param>
        /// <param name="recursive">delete all child to.</param>
        /// <returns>true if directory deleted.</returns>
        public static bool Delete(string dir, bool recursive)
        {
            try
            {
                System.IO.Directory.Delete(dir, recursive);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Move directory to new place.
        /// </summary>
        /// <param name="from">current location.</param>
        /// <param name="to">New location.</param>
        /// <returns>true or false.</returns>
        public static bool Move(string from, string to)
        {
            try
            {
                if (string.IsNullOrEmpty(from))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(to))
                {
                    return false;
                }

                if (string.Compare(from, to) == 0)
                {
                    return true;
                }

                System.IO.Directory.Move(from, to);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>.
        /// Directory exist or not.
        /// </summary>
        /// <param name="dir">Path.</param>
        /// <returns>True or false.</returns>
        public static bool Exist(string dir)
        {
            try
            {
                if (System.IO.Directory.Exists(dir))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Create directory.
        /// </summary>
        /// <param name="folder">Path to create.</param>
        /// <returns>true or false.</returns>
        public static bool DirectoryCreate(string folder)
        {
            try
            {
                if (!Exist(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get Size of directory.
        /// </summary>
        /// <param name="dir">Path to scan.</param>
        /// <param name="followChildren">Get space of children.</param>
        /// <param name="returnAs">Return as value.</param>
        /// <returns>Size of folder.</returns>
        public static ulong DirectoryGetSize(string dir, bool followChildren, LiTools.Helpers.Convert.FileSizeEnums returnAs)
        {
            if (string.IsNullOrEmpty(dir))
            {
                return 0;
            }

            if (!Exist(dir))
            {
                return 0;
            }

            System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(dir);

            ulong totalSize = DirectorySize(info, followChildren);

            if (returnAs == LiTools.Helpers.Convert.FileSizeEnums.Bytes)
            {
                return (ulong)totalSize;
            }

            var aa = LiTools.Helpers.Convert.Bytes.To(returnAs, (long)totalSize);

            return (ulong)aa;

            // return Convert.ToInt64(aa);
        }

        private static ulong DirectorySize(System.IO.DirectoryInfo dir, bool followChildren)
        {
            ulong totalSize = (ulong)dir.GetFiles().Sum(fi => fi.Length);

            if (followChildren)
            {
                var dd = dir.GetDirectories();
                foreach (var hej in dd)
                {
                    totalSize += DirectorySize(hej, followChildren);
                }

                // totalSize += dir.GetDirectories().Sum(di => this.DirectorySize(di, followChildren));
            }

            return totalSize;

            // return dir.GetFiles().Sum(fi => fi.Length) +
            //       dir.GetDirectories().Sum(di => DirectorySize(di, followChildren));
        }
    }
}
