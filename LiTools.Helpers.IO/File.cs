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
    using static System.Net.WebRequestMethods;

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

        /// <summary>
        /// Read All lines in file as strings.
        /// </summary>
        /// <param name="filename">filename to file to read.</param>
        /// <returns>true if file is read | data as string.</returns>
        public static Tuple<bool, string[]> ReadTextFileAllLines(string filename)
        {
            try
            {
                return new Tuple<bool, string[]>(true, System.IO.File.ReadAllLines(@filename));
            }
            catch (Exception)
            {
            }

            return new Tuple<bool, string[]>(false, new string[0]);
        }

        #endregion

        #region Write

        /// <summary>
        /// Write all lines to file.
        /// </summary>
        /// <param name="filename">Filname (and path) to write to.</param>
        /// <param name="lines">lines to write.</param>
        /// <param name="encoding">Enocding to use.</param>
        /// <returns>True if fale was saved.</returns>
        public static (bool Done, string Message) WriteAllLines(string filename, List<string> lines, Encoding encoding)
        {
            return WriteAllLines(filename, lines.ToArray(), encoding);
        }

        /// <summary>
        /// Write all lines to file.
        /// </summary>
        /// <param name="filename">Filname (and path) to write to.</param>
        /// <param name="lines">lines to write.</param>
        /// <param name="encoding">Enocding to use.</param>
        /// <returns>True if fale was saved.</returns>
        public static (bool Done, string Message) WriteAllLines(string filename, string[] lines, Encoding encoding)
        {
            try
            {
                System.IO.File.WriteAllLines(@filename, lines, encoding);
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

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
        public static (bool Done, string Message) Move(string from, string to)
        {
            try
            {
                if (string.IsNullOrEmpty(from))
                {
                    return (false, string.Empty);
                }

                if (string.IsNullOrEmpty(to))
                {
                    return (false, string.Empty);
                }

                if (string.Compare(from, to) == 0)
                {
                    return (false, string.Empty);
                }

                // File.Move(from, to);
                System.IO.File.Move(from, to);

                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        /// <summary>
        /// Move file to new location. dont use this. use move insted.
        /// </summary>
        /// <param name="from">path to file.</param>
        /// <param name="to">path to move the file info.</param>
        /// <returns>true or false.</returns>
        public static bool MoveOld(string from, string to)
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
        /// Move file to new location. dont use this. use move insted.
        /// </summary>
        /// <param name="from">path to file.</param>
        /// <param name="to">path to move the file info.</param>
        /// <param name="message">if error exist. error message.</param>
        /// <returns>true or false.</returns>
        public static bool MoveOld(string from, string to, out string message)
        {
            message = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(from))
                {
                    message = "from is null or empty";
                    return false;
                }

                if (string.IsNullOrEmpty(to))
                {
                    message = "to is null or empty";
                    return false;
                }

                if (string.Compare(from, to) == 0)
                {
                    message = "from and to are the same";
                    return false;
                }

                // File.Move(from, to);
                System.IO.File.Move(from, to);

                message = "file moved";
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
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
        /// Get file name from file path.
        /// </summary>
        /// <param name="path">folder to extract file from.</param>
        /// <returns>filename as string.</returns>
        public static string GetFilenameFromFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            var tmpFile = Path.GetFileName(path);

            if (string.IsNullOrEmpty(tmpFile))
            {
                return string.Empty;
            }

            return tmpFile;
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
