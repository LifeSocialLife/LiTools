// <summary>
// Webpage return as string model.
// </summary>
// <copyright file="WebpageReturnAsStringModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

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
        /// Gets or sets a value indicating whether is the page we are reading from working.
        /// </summary>
        public bool IsWorking { get; set; }

        /// <summary>
        /// Gets or sets return from page as string.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets do we have any error messages from page we are reading from.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
