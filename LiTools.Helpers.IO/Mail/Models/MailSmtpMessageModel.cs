// <summary>
// Mail outgoing model.
// </summary>
// <copyright file="MailSmtpMessageModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO.Mail.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Mail smtp message model.
    /// </summary>
    public class MailSmtpMessageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailSmtpMessageModel"/> class.
        /// </summary>
        public MailSmtpMessageModel()
        {
            this.From = string.Empty;
            this.To = new List<string>();
            this.Subject = string.Empty;
            this.Body = string.Empty;
            this.BodyIsHtml = true;
        }

        /// <summary>
        /// Gets or sets mail from.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets mail to as list.
        /// </summary>
        public List<string> To { get; set; }

        /// <summary>
        /// Gets or sets mail subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets mail body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is mail body html format?.
        /// </summary>
        public bool BodyIsHtml { get; set; }
    }
}
