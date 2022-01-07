// <summary>
// File Helper.
// </summary>
// <copyright file="File.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;

    /// <summary>
    /// File helper.
    /// </summary>
    public static class File
    {
        #region Read

        /// <summary>
        /// Read file as byte.
        /// </summary>
        /// <param name="filename">filename to read.</param>
        /// <returns>return as byte.</returns>
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
        /// Read file as byte. Start reading from input pos.
        /// </summary>
        /// <param name="filename">Filename to read.</param>
        /// <param name="from">seek position to start read.</param>
        /// <param name="len">lengt to read from file.</param>
        /// <returns>true/false | byte from file if it was true.</returns>
        public static Tuple<bool, byte[]> ReadBinaryFile(string filename, int from, int len)
        {
            try
            {
                if (len < 1)
                {
                    return new Tuple<bool, byte[]>(false, new byte[0]);
                }

                if (from < 0)
                {
                    return new Tuple<bool, byte[]>(false, new byte[0]);
                }

                byte[] ret = new byte[len];
                using (BinaryReader reader = new(new FileStream(filename, System.IO.FileMode.Open)))
                {
                    reader.BaseStream.Seek(from, SeekOrigin.Begin);
                    reader.Read(ret, 0, len);
                }

                return new Tuple<bool, byte[]>(true, ret);
            }
            catch (Exception)
            {
                return new Tuple<bool, byte[]>(false, new byte[0]);
            }
        }

        /// <summary>
        /// Read file as string.
        /// </summary>
        /// <param name="filename">filename to file to read.</param>
        /// <returns>true if file is read | data as string.</returns>
        public static Tuple<bool, string> ReadTextFile(string filename)
        {
            try
            {
                return new Tuple<bool, string>(true, System.IO.File.ReadAllText(@filename));
            }
            catch (Exception)
            {
            }

            return new Tuple<bool, string>(false, string.Empty);
        }

        #endregion

        #region Write

        /// <summary>
        /// Write file.
        /// </summary>
        /// <param name="filename">Name of file.</param>
        /// <param name="content">data as string.</param>
        /// <param name="append">replace or append data if file already exist.</param>
        /// <returns>true if everthing was ok.</returns>
        public static bool WriteFile(string filename, string content, bool append)
        {
            using (StreamWriter writer = new(filename, append))
            {
                writer.WriteLine(content);
            }

            return true;
        }

        /// <summary>
        /// Write file.
        /// </summary>
        /// <param name="filename">FileInfo.</param>
        /// <param name="content">data as string.</param>
        /// <param name="append">replace or append data if file already exist.</param>
        /// <returns>true if everthing was ok.</returns>
        public static bool WriteFile(FileInfo filename, string content, bool append)
        {
            return WriteFile(filename.FullName, content, append);
        }

        /// <summary>
        /// Write file.
        /// </summary>
        /// <param name="filename">Name of file.</param>
        /// <param name="content">data as byte[].</param>
        /// <returns>true if everthing was ok.</returns>
        public static bool WriteFile(string filename, byte[] content)
        {
            if (content != null && content.Length > 0)
            {
                System.IO.File.WriteAllBytes(filename, content);
            }
            else
            {
                System.IO.File.Create(filename).Close();
            }

            return true;
        }

        /// <summary>
        /// Write file.
        /// </summary>
        /// <param name="filename">Name of file.</param>
        /// <param name="content">data as byte[].</param>
        /// <param name="pos">where in the file shod this be saved.</param>
        /// <returns>true if everthing was ok.</returns>
        public static bool WriteFile(string filename, byte[] content, int pos)
        {
            using (Stream stream = new FileStream(filename, System.IO.FileMode.OpenOrCreate))
            {
                stream.Seek(pos, SeekOrigin.Begin);
                stream.Write(content, 0, content.Length);
            }

            return true;
        }

        #endregion

        #region Tools

        /// <summary>
        /// Check if a file exist.
        /// </summary>
        /// <param name="file">file path.</param>
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

        /// <summary>
        /// Move file to new location.
        /// </summary>
        /// <param name="from">path to file.</param>
        /// <param name="to">path to move the file info.</param>
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

                // File.Move(from, to);
                System.IO.File.Move(from, to);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Rename file.
        /// </summary>
        /// <param name="from">path to file.</param>
        /// <param name="to">New name whit path.</param>
        /// <returns>true or false.</returns>
        public static bool Rename(string from, string to)
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

                System.IO.File.Move(from, to);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete file.
        /// </summary>
        /// <param name="filename">path to file.</param>
        /// <returns>true if file was removed.</returns>
        public static bool DeleteFile(string filename)
        {
            try
            {
                System.IO.File.Delete(@filename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get Directory Path from file path.
        /// </summary>
        /// <param name="file">path to file.</param>
        /// <returns>Path to directory.</returns>
        public static string GetFolderFromFilePath(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                return string.Empty;
            }

            var tmpFolder = Path.GetDirectoryName(file);

            if (string.IsNullOrEmpty(tmpFolder))
            {
                return string.Empty;
            }

            return tmpFolder;
        }

        /// <summary>
        /// Do we have read acess to this file.
        /// </summary>
        /// <param name="filename">filename.</param>
        /// <returns>True = yes. we have read acess.</returns>
        public static bool VerifyFileReadAccess(string filename)
        {
            try
            {
                using FileStream stream = System.IO.File.Open(filename, System.IO.FileMode.Open, FileAccess.Read);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get file extension.
        /// </summary>
        /// <param name="filename">Path to file.</param>
        /// <returns>extension of file.</returns>
        public static string GetFileExtension(string filename)
        {
            try
            {
                if (string.IsNullOrEmpty(filename))
                {
                    return string.Empty;
                }

                return Path.GetExtension(filename);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion

    }
}
