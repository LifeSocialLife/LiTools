// <summary>
// StringLines.
// </summary>
// <copyright file="StringLines.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Generate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Generate strings.
    /// </summary>
    public static class StringLines
    {
        private const string CharsUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string CharsLower = "abcdefghijklmnopqrstuvwxyz";
        private const string CharsNumber = "0123456789";

        private static Random random = new Random();

        /// <summary>
        /// Generate random string. dont use this to generate passwords.
        /// </summary>
        /// <param name="length">Length of string.</param>
        /// <param name="includeUpperLetters">Shod be use upper letters.</param>
        /// <param name="includeLowerLetters">Shod be use lower letters.</param>
        /// <param name="includeNumbers">Shod we use numbers.</param>
        /// <returns>string.</returns>
        [Obsolete("Use new function Random() instead.")]
        public static string RandomString(int length, bool includeUpperLetters = true, bool includeLowerLetters = true, bool includeNumbers = true)
        {
            string tmpString = string.Empty;

            if (includeUpperLetters)
            {
                tmpString = CharsUpper;
            }

            if (includeLowerLetters)
            {
                tmpString += CharsLower;
            }

            if (includeNumbers)
            {
                tmpString += CharsNumber;
            }

            if (string.IsNullOrEmpty(tmpString))
            {
                tmpString = CharsUpper + CharsLower + CharsNumber;
            }

            // return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            // return new string(Enumerable.Range(1, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());
            return new string(Enumerable.Range(1, length).Select(_ => tmpString[random.Next(tmpString.Length)]).ToArray());
        }

        /// <summary>
        /// Generate random string. using RandomNumberGenerator.
        /// </summary>
        /// <param name="length">Length of string.</param>
        /// <param name="characterSet">Character to set in string.</param>
        /// <returns>string.</returns>
        public static string Random(int length, string characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
        {
            char[] chars = new char[length];
            byte[] randomBytes = new byte[length];

            RandomNumberGenerator.Fill(randomBytes);

            for (int i = 0; i < length; i++)
            {
                chars[i] = characterSet[randomBytes[i] % characterSet.Length];
            }

            return new string(chars);
        }
    }
}
