// <summary>
// MongoDb Rundata service.
// </summary>
// <copyright file="MongoDbService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (Lempa)</author>

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
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using LiTools.Helpers.DataAccess.MongoDb.Helpers;
    using LiTools.Helpers.DataAccess.MongoDb.Models;
    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>
    /// Mongo db Runtime service.
    /// </summary>
    public class MongoDbService
    {
        /// <summary>
        /// MongoDB maximum document size in bytes (16 MB).
        /// </summary>
        private const int MONGO_MAX_DOCUMENT_SIZE = 16 * 1024 * 1024; // 16,777,216 bytes

        /// <summary>
        /// Safe threshold for MongoDB document size in bytes (15 MB).
        /// Provides buffer for metadata and prevents edge-case rejections.
        /// </summary>
        private const int MONGO_SAFE_THRESHOLD = 15 * 1024 * 1024; // 15,728,640 bytes

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
            else if (ex is OperationCanceledException oce)
            {
                // Handle cancellation scenario
                if (oce.CancellationToken.IsCancellationRequested)
                {
                    this.zzDebug = "Task was canceled";
#if DEBUG
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }
#endif
                }
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

            await Task.Delay(1).ConfigureAwait(false);
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
        /// Get size of model as BSON bytes.
        /// </summary>
        /// <remarks>
        /// Converts the model to a BSON document and returns the serialized byte size.
        /// This accurately reflects how MongoDB will store the document.
        /// Uses the runtime type of the object to ensure the correct BSON serializer is used.
        /// </remarks>
        /// <param name="data">The model to get size from. Cannot be null.</param>
        /// <returns>Size as bytes.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is null.</exception>
        public int GetModelSizeAsBsonAsBytes(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            // Use runtime type to get the correct BSON serializer
            // This is necessary because the compile-time type is 'object',
            // but we need the actual type (CupsMdbModel, etc.) to find the right serializer
            var runtimeType = data.GetType();
            var bsonDocument = data.ToBsonDocument(runtimeType);
            int bytesSize = bsonDocument.ToBson().Length;
            return bytesSize;
        }

        /// <summary>
        /// Determines whether a model can be safely stored in MongoDB.
        /// </summary>
        /// <remarks>
        /// MongoDB has a maximum document size of 16 MB. This method checks if the model's BSON
        /// representation is below the safe threshold of 15 MB to allow room for metadata.
        /// </remarks>
        /// <param name="data">The model to check. Cannot be null.</param>
        /// <returns>
        /// <c>true</c> if the model size is below 15 MB and can be stored; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is null.</exception>
        public bool ModelSizeFit(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            int size = this.GetModelSizeAsBsonAsBytes(data);

            // Safe threshold: 15 MB (leaves 1 MB buffer for metadata and other fields)
            // MongoDB max document size: 16 MB (16,777,216 bytes)
            return size < MONGO_SAFE_THRESHOLD;
        }

        /// <summary>
        /// Gets the BSON serialized size of a model in bytes.
        /// </summary>
        /// <remarks>
        /// This method serializes the model to BSON format and returns the exact byte size
        /// that MongoDB will use to store the document.
        /// </remarks>
        /// <param name="data">The model to measure. Cannot be null.</param>
        /// <returns>The size of the model in bytes when serialized as BSON.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is null.</exception>
        public int ModelSizeInBytes(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return this.GetModelSizeAsBsonAsBytes(data);
        }

        /// <summary>
        /// Gets the BSON serialized size of a model in kilobytes (KB).
        /// </summary>
        /// <remarks>
        /// This method serializes the model to BSON format and converts the size to kilobytes.
        /// One kilobyte = 1,024 bytes.
        /// </remarks>
        /// <param name="data">The model to measure. Cannot be null.</param>
        /// <returns>The size of the model in kilobytes (KB) when serialized as BSON.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is null.</exception>
        public double ModelSizeInKb(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            int sizeInBytes = this.GetModelSizeAsBsonAsBytes(data);
            double sizeInKb = sizeInBytes / 1024.0;
            return sizeInKb;
        }

        /// <summary>
        /// Gets the BSON serialized size of a model in megabytes (MB).
        /// </summary>
        /// <remarks>
        /// This method serializes the model to BSON format and converts the size to megabytes.
        /// One megabyte = 1,048,576 bytes (1024 * 1024).
        /// </remarks>
        /// <param name="data">The model to measure. Cannot be null.</param>
        /// <returns>The size of the model in megabytes (MB) when serialized as BSON.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is null.</exception>
        public double ModelSizeInMb(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            int sizeInBytes = this.GetModelSizeAsBsonAsBytes(data);
            double sizeInMb = sizeInBytes / (1024.0 * 1024.0);
            return sizeInMb;
        }
    }
}