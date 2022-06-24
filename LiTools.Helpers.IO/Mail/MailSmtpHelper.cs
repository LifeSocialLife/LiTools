// <summary>
// Smtp Server helper.
// </summary>
// <copyright file="MailSmtpHelper.cs" company="LiSoLi">
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
    /// Smtp helper.
    /// </summary>
    public class MailSmtpHelper
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MailSmtpHelper"/> class.
        /// </summary>
        public MailSmtpHelper()
        {
            this.zzDebug = "MailSmtpHelper";

        }

        private string zzDebug { get; set; }

    }
}
