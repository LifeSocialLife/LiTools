// <summary>
// MongoDb Server Register Model.
// </summary>
// <copyright file="ServerRegisterModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.DataAccess.MongoDb.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// MongoDb Server Register Model.
    /// </summary>
    public class ServerRegisterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerRegisterModel"/> class.
        /// </summary>
        public ServerRegisterModel()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.Hostname = string.Empty;
            this.Port = 27017;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.AuthSource = string.Empty;
            this.ReadPreference = true;
            this.UseSsl = false;
        }

        /// <summary>
        /// Gets or sets name of this mongodb server.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description of this mongodb.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets hostname or ip adress of this mongodb server.
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets port to use.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets where username and password whit be authenticated.
        /// </summary>
        public string AuthSource { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets ReadPreference.
        /// </summary>
        public bool ReadPreference { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets usessl, shod connection use ssl.
        /// </summary>
        public bool UseSsl { get; set; }
    }
}
