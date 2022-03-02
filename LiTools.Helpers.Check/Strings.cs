// <summary>
// Check string data.
// </summary>
// <copyright file="Strings.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Check
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Check string for diffrent data.
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// Check if input string is an ipaddress. Can check both v4 and v6.
        /// </summary>
        /// <param name="input">string data.</param>
        /// <returns>True if it is an ipadress as input.</returns>
        public static bool IsIpAddress(string input)
        {
            // System.Net.IPAddress? ip;
            return IsIpAddress(input, out _);
        }

        /// <summary>
        /// Check if input string is an ipaddress. Can check both v4 and v6.
        /// </summary>
        /// <param name="input">string data.</param>
        /// <param name="address">input string as output IpAddress.</param>
        /// <returns>True if it is an ipadress as input.</returns>
        public static bool IsIpAddress(string input, out System.Net.IPAddress? address)
        {
            if (System.Net.IPAddress.TryParse(input, out address))
            {
                return true;
                ////switch (address.AddressFamily)
                ////{
                ////    case System.Net.Sockets.AddressFamily.InterNetwork:
                ////        // we have IPv4
                ////        break;
                ////    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                ////        // we have IPv6
                ////        break;
                ////    default:
                ////        // umm... yeah... I'm going to need to take your red packet and...
                ////        break;
                ////}
            }

            return false;
        }

        #region Contains Number, Lower and Upper Casts or only numbers.

        /// <summary>
        /// Contains only numbers?.
        /// </summary>
        /// <param name="input">string.</param>
        /// <returns>true or false.</returns>
        public static bool ContainsOnlyNumbers(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return input.All(char.IsDigit);
        }

        /// <summary>
        /// Contains numbers?.
        /// </summary>
        /// <param name="input">string.</param>
        /// <returns>true or false.</returns>
        public static bool ContainsNumbers(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return input.Any(char.IsDigit);
        }

        /// <summary>
        /// Contains Lower casts?.
        /// </summary>
        /// <param name="input">string.</param>
        /// <returns>true or false.</returns>
        public static bool ContainsLowerCasts(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return input.Any(char.IsLower);
        }

        /// <summary>
        /// Contains Upper casts?.
        /// </summary>
        /// <param name="input">string.</param>
        /// <returns>true or false.</returns>
        public static bool ContainsUpperCasts(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return input.Any(char.IsUpper);
        }

        #endregion

        /// <summary>
        /// string contains sql injections code.
        /// </summary>
        /// <param name="input">string.</param>
        /// <returns>true or false.</returns>
        public static bool SqlSafe(string input)
        {
            if (input.Contains("'"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if email adress is a email adress.
        /// </summary>
        /// <param name="email">email adr as string.</param>
        /// <returns>true or false.</returns>
        public static bool IsMailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            email = email.Trim();

            if (!IsMailValidCheck1(email))
            {
                return false;
            }

            if (!IsMailValidCheck2(email))
            {
                return false;
            }

            if (!IsMailValidCheck3(email))
            {
                return false;
            }

            return true;
        }

        private static bool IsMailValidCheck1(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsMailValidCheck2(string email)
        {
            // System.ComponentModel.DataAnnotations;
            // -
            var foo = new EmailAddressAttribute();
            return foo.IsValid(email);
        }

        private static bool IsMailValidCheck3(string email)
        {
            // https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
            // -
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}