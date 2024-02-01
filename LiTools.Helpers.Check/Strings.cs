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
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Check string for different data.
    /// </summary>
    public static class Strings
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");

        /// <summary>
        /// Replace whitespaces whit something else.
        /// </summary>
        /// <param name="input">string to check.</param>
        /// <param name="replacement">replace whit this.</param>
        /// <returns>string whit whitespaces replaced.</returns>
        public static string WhitespaceReplaced(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }

        /// <summary>
        /// Remove whitespaces from string.
        /// </summary>
        /// <param name="input">string that we shod remove whitespaces from.</param>
        /// <returns>string whiteout whitespaces.</returns>
        public static string WhitespaceRemoved(string input)
        {
            return WhitespaceReplaced(input, string.Empty);
        }

        /// <summary>
        /// Check if input string is an ip address. Can check both v4 and v6.
        /// </summary>
        /// <param name="input">string data.</param>
        /// <returns>True if it is an ip adress as input.</returns>
        public static bool IsIpAddress(string input)
        {
            // System.Net.IPAddress? ip;
            return IsIpAddress(input, out _);
        }

        /// <summary>
        /// Check if input string is an ip address. Can check both v4 and v6.
        /// </summary>
        /// <param name="input">string data.</param>
        /// <param name="address">input string as output IpAddress.</param>
        /// <returns>True if it is an ip adress as input.</returns>
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

        #region Guid

        /// <summary>
        /// Check if a string is Guid.
        /// </summary>
        /// <param name="input">string to check.</param>
        /// <returns>true or false.</returns>
        public static bool IsGuId(string input)
        {
            if (Guid.TryParse(input, out _))
            {
                // input is a valid Guid
                return true;
            }
            else
            {
                // input is not a valid Guid
                return false;
            }
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
        /// String shortened to the length of input max length.
        /// </summary>
        /// <param name="input">string to shortened.</param>
        /// <param name="maxLength">string max length.</param>
        /// <returns>string as right length.</returns>
        public static string StringShortened(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "no input text";
            }

            if (maxLength <= 0)
            {
                return "Error - StringShortened max length";
            }

            if (input.Length <= maxLength)
            {
                return input;
            }

            string[] words = input.Split(' ');

            if (words.Length == 1)
            {
                // If there are no spaces, split at index maxLength.
                // return input.Substring(0, maxLength);
                return input[..maxLength];
            }
            else
            {
                string result = string.Empty;
                int lengthCount = 0;

                foreach (string word in words)
                {
                    if (lengthCount + word.Length > maxLength)
                    {
                        break;
                    }

                    result += word + " ";
                    lengthCount += word.Length + 1; // +1 for the space character
                }

                return result.Trim();
            }
        }

        #region Mail checks

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

        #endregion

        #region Http web adress checks

        /// <summary>
        /// Check if string is an url path.
        /// </summary>
        /// <param name="input">url as string.</param>
        /// <returns>true or false.</returns>
        [Obsolete("Use UrlIsValid instead.", false)]
        [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:ElementsMustBeOrderedByAccess", Justification = "Reviewed.")]
        public static bool IsHttpOrHttpsUrl(string input)
        {
            if (Uri.TryCreate(input, UriKind.Absolute, out Uri uriResult))
            {
                if (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
                {
                    return true;
                }

                // If the scheme is missing, you can assume HTTP by default:
                if (uriResult.IsAbsoluteUri)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if string is an url path.
        /// </summary>
        /// <param name="input">url as string.</param>
        /// <returns>True or False.</returns>
        public static bool UrlIsValid(string input)
        {
            // if (input.ToLower().StartsWith("http://") || input.ToLower().StartsWith("http://")
            bool containsHttpOrHttps = UrlIsValidScheme(input);
            if (!containsHttpOrHttps)
            {
                input = "http://" + input;
            }

            // Use Uri.TryCreate to check if the string is a valid URL
            if (Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out Uri uriResult))
            {
                // Additional checks if needed
                return UrlIsValidScheme(uriResult.Scheme) && UrlIsValidDomain(uriResult.Host);
            }

            return false;
            /*
            if (Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out Uri uriResult))
            {
                // Ensure it's an absolute URI and has either "http" or "https" scheme
                return uriResult.IsAbsoluteUri && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }

            return false;
            */
        }

        private static bool UrlIsValidScheme(string scheme)
        {
            // Optional: Check if the URL has a valid scheme (http or https)
            return scheme.Equals(Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) ||
                   scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);
        }

        private static bool UrlIsValidDomain(string host)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                return false;
            }

            // Check for a minimum and maximum length of the domain
            if (host.Length < 3 || host.Length > 255)
            {
                return false;
            }

            // Check if the domain contains a dot for subdomains
            if (!host.Contains('.'))
            {
                return false;
            }

            // Check if the domain starts and ends with a letter or digit
            if (!char.IsLetterOrDigit(host[0]) || !char.IsLetterOrDigit(host[host.Length - 1]))
            {
                return false;
            }

            // Add more specific checks based on your requirements
            return true;
        }
        #endregion
    }
}