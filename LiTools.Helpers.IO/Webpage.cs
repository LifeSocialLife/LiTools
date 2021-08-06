// <summary>
// Webpage IO helper.
// </summary>
// <copyright file="Webpage.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.SymbolStore;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Webpage helper.
    /// </summary>
    public static class Webpage
    {
        /// <summary>
        /// Read page and return as string.
        /// </summary>
        /// <param name="url">page to read.</param>
        /// <returns>page as string.</returns>
        public static async Task<WebpageReturnAsStringModel> ReturnAsString(string url)
        {
            var tmpReturn = new WebpageReturnAsStringModel();

            HttpClientHandler handler = new()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None,
            };

            System.Net.Http.HttpClient hc = new(handler);

            try
            {
                System.Net.Http.HttpResponseMessage response = await hc.GetAsync(new Uri(url, UriKind.Absolute));
                tmpReturn.Source = await response.Content.ReadAsStringAsync();
                tmpReturn.IsWorking = true;
            }
            catch (Exception e)
            {
                // Error connecting using https.
                tmpReturn.ErrorMessage = e.Message;
                tmpReturn.IsWorking = false;
            }

            return tmpReturn;
        }
    }
}
