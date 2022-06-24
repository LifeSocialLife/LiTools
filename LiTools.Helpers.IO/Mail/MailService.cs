// <summary>
// Mail service.
// </summary>
// <copyright file="MailService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.IO.Mail
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Mail Service.
    /// </summary>
    public class MailService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailService"/> class.
        /// </summary>
        public MailService()
        {
            this.zzDebug = "MailService";
            this.Servers = new ConcurrentDictionary<string, MailServersModel>();
        }

        private string zzDebug { get; set; }

        private ConcurrentDictionary<string, MailServersModel> Servers { get; set; }

        #region Server Add, check and remove

        /// <summary>
        /// Add new server to mail servers list.
        /// </summary>
        /// <param name="data">MailServerRegisterModel as list.</param>
        public void ServerAdd(List<MailServerRegisterModel>? data)
        {
            if (data == null)
            {
                return;
            }

            if (data.Count == 0)
            {
                return;
            }

            foreach (var item in data)
            {
                this.ServerAdd(item);
            }
        }

        /// <summary>
        /// Add new server to mail servers list.
        /// </summary>
        /// <param name="data">MailServerRegisterModel.</param>
        public void ServerAdd(MailServerRegisterModel data)
        {
            if (!this.ServerCheckSyntax(data))
            {
                return;
            }

            if (this.ServerExist(data.Name))
            {
                return;
            }

            this.Servers.TryAdd(data.Name, new MailServersModel()
            {
                RegData = data,
                InitIsDone = false,
                SmtpError = false,
            });

            this.Init();
        }

        /// <summary>
        /// Check server registering data.
        /// </summary>
        /// <param name="data">MailServersModel.</param>
        /// <returns>True if input syntax is correct.</returns>
        public bool ServerCheckSyntax(MailServerRegisterModel data)
        {
            if (data == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(data.Name))
            {
                return false;
            }

            if (data.SmtpActivated)
            {
                if (string.IsNullOrEmpty(data.SmtpHostname) || data.SmtpPort == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Do node already exist?.
        /// </summary>
        /// <param name="name">name of node.</param>
        /// <returns>trur or false.</returns>
        public bool ServerExist(string name)
        {
            if (this.Servers.ContainsKey(name))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Init and Mgn work

        /// <summary>
        /// Do init on all mail server connections.
        /// </summary>
        /// <param name="foreRebuild">Shod we force the database connection build.</param>
        public void Init(bool foreRebuild = false)
        {
            if (this.Servers == null)
            {
                return;
            }

            if (this.Servers.IsEmpty)
            {
                return;
            }

            foreach (var node in this.Servers)
            {
                if (!node.Value.InitIsDone || foreRebuild)
                {
                    node.Value.InitIsDone = true;

                    // TODO do init of nodes that is note done.
                }
            }
        }

        /// <summary>
        /// Do mgn work.
        /// </summary>
        /// <returns>task return data.</returns>
        public async Task MgnWork()
        {
            // await this.servers.Rebuild(true);
            this.zzDebug = "sdfd";
            await Task.Delay(100);
        }

        #endregion
    }
}
