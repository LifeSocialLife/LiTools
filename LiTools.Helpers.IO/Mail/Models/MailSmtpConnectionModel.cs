// <summary>
// Mail outgoing model.
// </summary>
// <copyright file="MailSmtpConnectionModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO.Mail.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Mail Smtp Connection Model.
    /// </summary>
    public class MailSmtpConnectionModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailSmtpConnectionModel"/> class.
        /// </summary>
        public MailSmtpConnectionModel()
        {
            this.SmtpPort = 0;
            this.SmtpAdr = string.Empty;
            this.UseSsl = false;
            this.UseCredentials = false;
            this.Credential = new MailSmtpConnectionCredentialsModel();
        }

        /// <summary>
        /// Gets or sets adress to smtp server.
        /// </summary>
        public string SmtpAdr { get; set; }

        /// <summary>
        /// Gets or sets smtp port.
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shod be use ssl to connect to this.
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shod we use credentials to connect to this smtp server.
        /// </summary>
        public bool UseCredentials { get; set; }

        /// <summary>
        /// Gets or sets credemtoals.
        /// </summary>
        public MailSmtpConnectionCredentialsModel Credential { get; set; }
    }
}
