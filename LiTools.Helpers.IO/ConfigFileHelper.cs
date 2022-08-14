// <summary>
// ConfigFile Helper.
// </summary>
// <copyright file="ConfigFileHelper.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Newtonsoft.Json;

    /// <summary>
    /// Read and Write model to Json file.
    /// </summary>
    public static class ConfigFileHelper
    {
        /// <summary>
        /// Gets or sets latest debug message.
        /// </summary>
        public static string? zzDebug { get; set; }

        /// <summary>
        /// Writes the given object instance to a Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        /// <returns>True if write of file was done.</returns>
        public static bool WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false)
            where T : new()
        {
            TextWriter? writer = null;
            bool tmpReturn = false;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            catch (Exception ex)
            {
                tmpReturn = false;
                zzDebug = ex.Message;

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }

                tmpReturn = true;
            }

            return tmpReturn;
        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public static T ReadFromJsonFile<T>(string filePath)
            where T : new()
        {
            TextReader? reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();

                var tmpreturn = JsonConvert.DeserializeObject<T>(fileContents);

                if (tmpreturn == null)
                {
                    return new T();
                }

                // return JsonConvert.DeserializeObject<T>(fileContents);
                return tmpreturn;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
    }
}
