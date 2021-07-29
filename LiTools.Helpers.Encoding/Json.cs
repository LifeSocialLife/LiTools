// <summary>
// Rundata Service.
// </summary>
// <copyright file="Json.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Encoding
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Json helper.
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Model to json string.
        /// </summary>
        /// <param name="obj">Model.</param>
        /// <param name="pretty">Use pretty output.</param>
        /// <returns>Model as json string.</returns>
        public static string Serialize(object obj, bool pretty)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            string tmpjson;

            if (pretty)
            {
                tmpjson = JsonConvert.SerializeObject(
                  obj,
                  Newtonsoft.Json.Formatting.Indented,
                  new JsonSerializerSettings
                  {
                      NullValueHandling = NullValueHandling.Ignore,
                      DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                  });
            }
            else
            {
                tmpjson = JsonConvert.SerializeObject(
                    obj, new JsonSerializerSettings
                  {
                      NullValueHandling = NullValueHandling.Ignore,
                      DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                  });
            }

            return tmpjson;
        }

        /// <summary>
        /// Json string to model.
        /// </summary>
        /// <typeparam name="T">model.</typeparam>
        /// <param name="json">json model as string.</param>
        /// <returns>model T.</returns>
        public static T Deserialize<T>(string json)
        {
#pragma warning disable CS8603 // Possible null reference return.
                return JsonConvert.DeserializeObject<T>(json);
#pragma warning restore CS8603 // Possible null reference return.
        }

        /// <summary>
        /// Json byte to model.
        /// </summary>
        /// <typeparam name="T">model.</typeparam>
        /// <param name="data">json model as byte.</param>
        /// <returns>Model T.</returns>
        public static T Deserialize<T>(byte[] data)
        {
            if (data == null || data.Length < 1)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return Deserialize<T>(Encoding.UTF8.GetString(data));
        }
    }
}
