// <summary>
// Directory helper.
// </summary>
// <copyright file="Directory.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
                List<string> folderList = new();

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
        /// Get all files inside directory.
        /// </summary>
        /// <param name="directory">Directory to scan.</param>
        /// <param name="returnCompletPath">Return whit directory information or not.</param>
        /// <returns>true if try work or false if catch happends. filelist as list.</returns>
        [Obsolete("GetFilesInsideDirectory is deprecated, please use GetFiles instead.", false)]
        public static Tuple<bool, List<string>> GetFilesInsideDirectory(string directory, bool returnCompletPath)
        {
            try
            {
                DirectoryInfo info = new(directory);
                FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
                List<string> fileList = new();

                foreach (FileInfo file in files)
                {
                    if (returnCompletPath)
                    {
                        fileList.Add(directory + "/" + file.Name);
                    }
                    else
                    {
                        fileList.Add(file.Name);
                    }
                }

                return new Tuple<bool, List<string>>(true, fileList);
            }
            catch (Exception)
            {
                return new Tuple<bool, List<string>>(false, new List<string>());
            }
        }

        /// <summary>
        /// Get files from a directory.
        /// </summary>
        /// <param name="directory">Directory to scan.</param>
        /// <param name="returnCompletePath">Return whit directory information or not.</param>
        /// <param name="orderbyFileCreationTime">Order files by creating time = true. if false it will be by name of file.</param>
        /// <returns>bool Success, string Message, List string FileList.</returns>
        public static (bool Success, string Message, List<string> FileList) GetFiles(string directory, bool returnCompletePath, bool orderbyFileCreationTime)
        {
            try
            {
                DirectoryInfo info = new(directory);
                FileInfo[] files; // = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();

                if (orderbyFileCreationTime)
                {
                    files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
                }
                else
                {
                    files = info.GetFiles().OrderBy(p => p.Name).ToArray();
                }

                if (returnCompletePath)
                {
                    return new(true, string.Empty, files.Select(p => directory + "/" + p.Name).ToList());
                }
                else
                {
                    return new(true, string.Empty, files.Select(p => p.Name).ToList());
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, new List<string>());
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

        /// <summary>
        /// Rename directory. this is the same as Move. Use move insted.
        /// </summary>
        /// <param name="from">current location.</param>
        /// <param name="to">New location.</param>
        /// <returns>true or false.</returns>
        public static bool Rename(string from, string to)
        {
            return LiTools.Helpers.IO.Directory.Move(from, to);
        }

        /// <summary>
        /// Combines two strings into a path.
        /// </summary>
        /// <param name="path1">The first path to combine.</param>
        /// <param name="path2">The second path to combine.</param>
        /// <returns> The combined paths. If one of the specified paths is a zero-length string, this method returns the other path. If path2 contains an absolute path, this method returns path2.</returns>
        public static string Combine(string path1, string path2)
        {
            return System.IO.Path.Combine(path1, path2);
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
        [Obsolete("DirectoryCreate is deprecated, please use Create instead.")]
        public static bool DirectoryCreate(string folder)
        {
            return Directory.Create(folder);
        }

        /// <summary>
        /// Create directory.
        /// </summary>
        /// <param name="folder">Path to create.</param>
        /// <returns>true or false.</returns>
        public static bool Create(string folder)
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

            System.IO.DirectoryInfo info = new(dir);

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
