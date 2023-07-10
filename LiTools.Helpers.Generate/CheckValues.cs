// <summary>
// Generate check values for data.
// </summary>
// <copyright file="CheckValues.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>

namespace LiTools.Helpers.Generate
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Generate check values for data.
    /// </summary>
    public static class CheckValues
    {
        /// <summary>
        /// Generate MD5 hash for an string.
        /// </summary>
        /// <param name="input">string.</param>
        /// <returns>md5 hash as string.</returns>
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
