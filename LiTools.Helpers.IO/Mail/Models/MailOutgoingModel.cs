// <summary>
// Mail outgoing model.
// </summary>
// <copyright file="MailOutgoingModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO.Mail.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Mail outgoing message template.
    /// </summary>
    public class MailOutgoingModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailOutgoingModel"/> class.
        /// </summary>
        public MailOutgoingModel()
        {
            this.Connection = new MailSmtpConnectionModel();
            this.Message = new MailSmtpMessageModel();
        }

        /// <summary>
        /// Gets or sets smtp connection information.
        /// </summary>
        public MailSmtpConnectionModel Connection { get; set; }

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        public MailSmtpMessageModel Message { get; set; }
    }
}
