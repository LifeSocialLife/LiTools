// <summary>
// ServerHelperNodeModel.
// </summary>
// <copyright file="ServerHelperNodeModel.cs" company="LiSoLi">
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
    using MongoDB.Driver;

    /// <summary>
    /// ServerHelperNodeModel.
    /// </summary>
    public class ServerHelperNodeModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerHelperNodeModel"/> class.
        /// </summary>
        public ServerHelperNodeModel()
        {
            this.RegData = null;
            this.InitIsDone = false;
            this.StatusMgnRunning = false;
            this.StatusHasError = false;
        }

        /// <summary>
        /// Gets or sets serverRegisterModel information.
        /// </summary>
        public ServerRegisterModel RegData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether management work is running.
        /// </summary>
        public bool StatusMgnRunning { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is the error in this connection.
        /// </summary>
        public bool StatusHasError { get; set; }

        /// <summary>
        /// Gets a value indicating whether is this connection working?.
        /// </summary>
        public bool IsWorking
        {
            get
            {
                if (this.StatusMgnRunning || this.StatusHasError)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is init done on this server.
        /// </summary>
        public bool InitIsDone { get; set; }

        /// <summary>
        /// Gets or sets mongodb Client Connection.
        /// </summary>
        public IMongoClient MdbClient { get; set; }

        /// <summary>
        /// Gets or sets mongoDb Database connection.
        /// </summary>
        public IMongoDatabase MdbDatabase { get; set; }

        /// <summary>
        /// Gets or sets connection string.
        /// </summary>
        public string MdbConnectionString { get; set; }
    }
}
