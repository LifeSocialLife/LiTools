// <summary>
// Mail outgoing model.
// </summary>
// <copyright file="MailSmtpConnectionCredentialsModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO.Mail.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Mail smtp connection Credential Model.
    /// </summary>
    public class MailSmtpConnectionCredentialsModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailSmtpConnectionCredentialsModel"/> class.
        /// </summary>
        public MailSmtpConnectionCredentialsModel()
        {
            this.UserPw = string.Empty;
            this.UserName = string.Empty;
        }

        /// <summary>
        /// Gets or sets smtp server username.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets smtp server user password.
        /// </summary>
        public string UserPw { get; set; }
    }
}
