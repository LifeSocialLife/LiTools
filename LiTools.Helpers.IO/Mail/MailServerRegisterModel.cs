// <summary>
// Mail servers register Model.
// </summary>
// <copyright file="MailServerRegisterModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO.Mail
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Model to register mail server information.
    /// </summary>
    public class MailServerRegisterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailServerRegisterModel"/> class.
        /// </summary>
        public MailServerRegisterModel()
        {
            this.Name = string.Empty;
            this.SmtpActivated = false;
            this.SmtpHostname = string.Empty;
            this.SmtpPort = 0;
        }

        /// <summary>
        /// Gets or sets name of this server.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shod smtp be used in this server.
        /// </summary>
        public bool SmtpActivated { get; set; }

        /// <summary>
        /// Gets or sets smtp server adress.
        /// </summary>
        public string SmtpHostname { get; set; }

        /// <summary>
        /// Gets or sets smtp port.
        /// </summary>
        public int SmtpPort { get; set; }
    }
}
