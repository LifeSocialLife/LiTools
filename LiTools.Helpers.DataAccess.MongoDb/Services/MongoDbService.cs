// <summary>
// MongoDb Rundata service.
// </summary>
// <copyright file="MongoDbService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

#region Help information.
/*  Mongo help information.

            var filter = Builders<Book>.Filter.Or(
            Builders<Book>.Filter.Where(p=>p.Title.ToLower().Contains(queryText.ToLower())),
            Builders<Book>.Filter.Where(p => p.Publisher.ToLower().Contains(queryText.ToLower())),
            Builders<Book>.Filter.Where(p => p.Description.ToLower().Contains(queryText.ToLower())));
            List<Book> books = Collection.Find(filter).ToList();


            use ReplaceOne when you are inserting or updating whole documents
            use UpdateOne when you need to update only a few properties/fields


 * */
#endregion

namespace LiTools.Helpers.DataAccess.MongoDb.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
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

        /// <summary>
        /// Get database to use.
        /// </summary>
        /// <returns>IMongoDatabase.</returns>
        public IMongoDatabase GetDatabaseToUse()
        {
            int count = 0;
            while (true)
            {
                var nodeToUse = this.servers.GetDatabaseToUse();
                if (nodeToUse == null)
                {
                    // TODO Generate error.
#if DEBUG
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }
#endif

                    if (count > 10)
                    {
                        // more then 5 sek. what shod we do now.
                        throw new InvalidOperationException("Failed to get a valid IMongoDatabase instance after multiple attempts.");
                    }

                    this.zzDebug = "aa";

                    Task.Delay(500);
                    count++;

                    continue;
                }

                return nodeToUse;
            }
        }

        /// <summary>
        /// Do init on all database connections.
        /// </summary>
        /// <param name="foreRebuild">Shod we force the database connection build.</param>
        public void Init(bool foreRebuild = false)
        {
            this.servers.Init();
        }

        /// <summary>
        /// Run mgn work.
        /// </summary>
        /// <returns>task done.</returns>
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
        /// Catch all function.
        /// </summary>
        /// <param name="ex">Exception.</param>
        /// <param name="callerMemberName">CallerMemberName.</param>
        /// <param name="callerFilePath">CallerFilePath.</param>
        /// <param name="callerLineNumber">CallerLineNumber.</param>
        /// <returns>Task done.</returns>
        public async Task CatchAll(
                Exception ex,
                [CallerMemberName] string callerMemberName = "",
                [CallerFilePath] string callerFilePath = "",
                [CallerLineNumber] int callerLineNumber = -1)
        {
            var callStack = ex?.StackTrace;

            if (ex is MongoWriteException e)
            {
                this.zzDebug = "MongoWriteException";
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif
            }
            else if (ex is MongoCommandException e1)
            {
                this.zzDebug = "MongoCommandException";
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif
            }
            else if (ex is MongoException e2)
            {
                this.zzDebug = "MongoException";
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif
            }
            else
            {
                this.zzDebug = "Exception";
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif
            }

            await Task.Delay(1);
        }

        /// <summary>
        /// ErrorHandlingWriteException.
        /// </summary>
        /// <param name="ex">MongoWriteException.</param>
        [Obsolete("use CatchAll()")]
        public void ErrorHandlingWriteException(MongoWriteException ex)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
#endif

            this.zzDebug = "dfdsf";
        }

        /// <summary>
        /// ErrorHandlingCommandException.
        /// </summary>
        /// <param name="ex">MongoCommandException.</param>
        [Obsolete("use CatchAll()")]
        public void ErrorHandlingCommandException(MongoCommandException ex)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
#endif

            this.zzDebug = "sdfdsf";
        }

        /// <summary>
        /// ErrorHandlingMongoException - MongoException.
        /// </summary>
        /// <param name="ex">MongoException.</param>
        [Obsolete("use CatchAll()")]
        public void ErrorHandlingMongoException(MongoException ex)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
#endif

            this.zzDebug = ex.ToString();
        }

        /// <summary>
        /// ErrorHandling.
        /// </summary>
        /// <param name="ex">Exception.</param>
        [Obsolete("use CatchAll()")]
        public void ErrorHandling(Exception ex)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
#endif

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
