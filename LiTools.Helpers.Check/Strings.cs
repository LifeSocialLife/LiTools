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
    using System.Net;
    using System.Text;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

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
    }
}
