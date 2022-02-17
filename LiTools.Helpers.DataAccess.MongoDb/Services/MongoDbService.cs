﻿// <summary>
// MongoDb Rundata service.
// </summary>
// <copyright file="MongoDbService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.DataAccess.MongoDb.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LiTools.Helpers.DataAccess.MongoDb.Helpers;
    using LiTools.Helpers.DataAccess.MongoDb.Models;
    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>
    /// Mongodb Runtime service.
    /// </summary>
    public class MongoDbService
    {
        private readonly ServerHelper servers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbService"/> class.
        /// </summary>
        public MongoDbService()
        {
            this.zzDebug = "MongoDbService";
            this.servers = new ServerHelper();
        }

        #region Connectionstrings and database name.

        /// <summary>
        /// Gets ConnectionString.
        /// </summary>
        public string ConnectionString => this.servers.ConnectionString;

        /// <summary>
        /// Gets ConnectionStringWrite.
        /// </summary>
        public string ConnectionStringWrite => this.servers.ConnectionStringWrite;

        /// <summary>
        /// Gets or sets database name of the connection.
        /// </summary>
        public string DatabaseName
        {
            get
            {
                return this.servers.DatabaseName;
            }

            set
            {
                this.servers.DatabaseName = value;
            }
        }

        /// <summary>
        /// Gets or sets Appname of this sortware.
        /// </summary>
        public string Appname
        {
            get
            {
                return this.servers.Appname;
            }

            set
            {
                this.servers.Appname = value;
            }
        }

        #endregion

        private string zzDebug { get; set; }

        /// <summary>
        /// Collect logs.
        /// </summary>
        public void CollectLogs()
        {
            _ = this.zzDebug;
        }

        public IMongoDatabase GetDatabaseToUse()
        {
            var nodeToUse = this.servers.GetDatabaseToUse();
            if (nodeToUse == null)
            {
                // TODO Generate error.
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                this.zzDebug = "dfdsf";
            }

            return nodeToUse;
        }

        /// <summary>
        /// Do init on all database connections.
        /// </summary>
        /// <param name="foreRebuild">Shod we force the database connection build.</param>
        public void Init(bool foreRebuild = false)
        {
            this.servers.Init();
        }

        public async Task MgnWork()
        {
            await this.servers.Rebuild(true);
            this.zzDebug = "sdfd";

        }

        /// <summary>
        /// Add new database server to nodes.
        /// </summary>
        /// <param name="data">ServerRegisterModel.</param>
        public void RegisterServerNode(ServerRegisterModel data)
        {
            if (data == null)
            {
                return;
            }

            this.servers.NodeAdd(data);
        }

        #region Error handling

        /// <summary>
        /// ErrorHandlingWriteException.
        /// </summary>
        /// <param name="ex">MongoWriteException.</param>
        public void ErrorHandlingWriteException(MongoWriteException ex)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }

            this.zzDebug = "dfdsf";
        }

        /// <summary>
        /// ErrorHandlingCommandException.
        /// </summary>
        /// <param name="ex">MongoCommandException.</param>
        public void ErrorHandlingCommandException(MongoCommandException ex)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }

            this.zzDebug = "sdfdsf";
        }

        /// <summary>
        /// ErrorHandling.
        /// </summary>
        /// <param name="ex">Exception.</param>
        public void ErrorHandling(Exception ex)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }

            this.zzDebug = "sdfdsf";
        }

        #endregion

        /// <summary>
        /// Get size of model.
        /// </summary>
        /// <param name="data">the model to get size from.</param>
        /// <returns>size as bytes.</returns>
        public int GetModelSizeAsBsonAsBytes(object data)
        {
            int bytesSize = data.ToBson().Length;
            return bytesSize;
        }

        /// <summary>
        /// Can this model be saved to database.
        /// </summary>
        /// <param name="data">the model to check if it can be stored in mongo.</param>
        /// <returns>true or false.</returns>
        public bool ModelSizeFit(object data)
        {
            int size = this.GetModelSizeAsBsonAsBytes(data);

            // 15,9 mb. Max size to store is 16mb.
            if (size < 15900000)
            {
                return true;
            }

            return false;
        }
    }
}