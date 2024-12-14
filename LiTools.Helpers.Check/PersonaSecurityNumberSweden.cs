// <summary>
// Persona Security Number - Sweden
// </summary>
// <copyright file="PersonaSecurityNumberSweden.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Check
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Check personal security number - Sweden.
    /// </summary>
    public static class PersonaSecurityNumberSweden
    {
        /*  Understand the Format
        A Swedish personal security number is typically in one of these formats:

        YYYYMMDD-XXXX (standard format)
        YYYYMMDD+XXXX (for individuals older than 100 years)

        Where:

        YYYYMMDD represents the date of birth.
        - or + separates the date of birth and the serial number.
        XXXX is a four-digit serial number, where the last digit is a checksum calculated using the Luhn algorithm.
        */

        /// <summary>
        /// Combine the Checks.
        /// </summary>
        /// <param name="personalNumber">Personal security number in {yyyymmdd-xxxx} format.</param>
        /// <returns>True or False.</returns>
        public static bool IsValidPersonalNumber(string personalNumber)
        {
            return IsValidFormat(personalNumber) &&
                   IsValidDate(personalNumber) &&
                   IsValidChecksum(personalNumber);
        }

        /// <summary>
        /// Check the Format.
        /// </summary>
        /// <param name="personalNumber">Personal security number in {yyyymmdd-xxxx} format.</param>
        /// <returns>True or false.</returns>
        public static bool IsValidFormat(string personalNumber)
        {
            string pattern = @"^\d{8}[-+]\d{4}$";
            return Regex.IsMatch(personalNumber, pattern);
        }

        /// <summary>
        /// Validate the Date
        /// Extract the YYYYMMDD part and ensure it represents a valid date. Consider the + sign, which indicates a birth date over 100 years ago.
        /// </summary>
        /// <param name="personalNumber">Personal security number in {yyyymmdd-xxxx} format.</param>
        /// <returns>True or False.</returns>
        public static bool IsValidDate(string personalNumber)
        {
            string datePart = personalNumber.Substring(0, 8);
            if (DateTime.TryParseExact(datePart, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validate the Checksum.
        /// The checksum is calculated using the Luhn algorithm on the digits of the number (excluding the - or +).
        /// </summary>
        /// <param name="personalNumber">Personal security number in {yyyymmdd-xxxx} format.</param>
        /// <returns>True or False.</returns>
        public static bool IsValidChecksum(string personalNumber)
        {
            if (string.IsNullOrEmpty(personalNumber))
            {
                return false;
            }

            string digits = personalNumber.Replace("-", string.Empty).Replace("+", string.Empty);

            // Ensure it is 12 digits after replacement
            if (digits.Length != 12)
            {
                return false;
            }

            int sum = 0;
            for (int i = 2; i <= 11; i++)
            {
                // Convert char to int
                int digit = digits[i] - '0';

                // Double every second digit
                if (i % 2 == 0)
                {
                    digit *= 2;
                }

                // Add both digits of two-digit numbers
                sum += (digit / 10) + (digit % 10);
            }

            return sum % 10 == 0;
        }
    }
}
