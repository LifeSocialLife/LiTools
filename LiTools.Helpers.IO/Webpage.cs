// <summary>
// Webpage IO helper.
// </summary>
// <copyright file="Webpage.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System.Collections.Generic;
    using System.Diagnostics.SymbolStore;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System;
    using System.Net;

    /// <summary>
    /// Webpage helper.
    /// </summary>
    public static class Webpage
    {
        /// <summary>
        /// Read page and return as string.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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

    /// <summary>
    /// Webpage return as string model.
    /// </summary>
    public class WebpageReturnAsStringModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebpageReturnAsStringModel"/> class.
        /// </summary>
        public WebpageReturnAsStringModel()
        {
            this.IsWorking = false;
            this.Source = string.Empty;
            this.ErrorMessage = string.Empty;
        }

        /// <summary>
        /// Is the page we are reading from working.
        /// </summary>
        public bool IsWorking { get; set; }
        /// <summary>
        /// Return from page as string.
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Do we have any error messages from page we are reading from.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
