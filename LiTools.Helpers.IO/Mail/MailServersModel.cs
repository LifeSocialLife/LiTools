// <summary>
// Mail servers Model.
// </summary>
// <copyright file="MailServersModel.cs" company="LiSoLi">
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
    /// Model to handle mail servers information.
    /// </summary>
    public class MailServersModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailServersModel"/> class.
        /// </summary>
        public MailServersModel()
        {
            this.RegData = new MailServerRegisterModel();
            this.InitIsDone = false;
            this.SmtpError = false;
        }

        /// <summary>
        /// Gets or sets server registering data.
        /// </summary>
        public MailServerRegisterModel RegData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether init is done.
        /// </summary>
        public bool InitIsDone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether smtp has reported errors.
        /// </summary>
        public bool SmtpError { get; set; }
    }
}
