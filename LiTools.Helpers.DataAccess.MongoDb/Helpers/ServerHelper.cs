// <summary>
// ServerHelper.
// </summary>
// <copyright file="ServerHelper.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.DataAccess.MongoDb.Helpers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LiTools.Helpers.DataAccess.MongoDb.Models;
    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>
    /// Server "node" helper.
    /// </summary>
    public class ServerHelper
    {
        private string _connectionString;
        private string _connectionStringWrite;
        private string _databaseName;
        private string _appnName;
        private bool _rebuldForceFull;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerHelper"/> class.
        /// </summary>
        public ServerHelper()
        {
            this.zzDebug = "ServerHelper";
            this.Nodes = new ConcurrentDictionary<string, ServerHelperNodeModel>();
            this._connectionStringWrite = string.Empty;
            this._connectionString = string.Empty;
            this._databaseName = string.Empty;
            this._appnName = "noname";
            this._rebuldForceFull = false;
        }

        #region Connectionstrings and database name, appname

        /// <summary>
        /// Gets ConnectionString.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
        }

        /// <summary>
        /// Gets ConnectionStringWrite.
        /// </summary>
        public string ConnectionStringWrite
        {
            get
            {
                return this._connectionStringWrite;
            }
        }

        /// <summary>
        /// Gets or sets database name of the connection.
        /// </summary>
        public string DatabaseName
        {
            get
            {
                return this._databaseName;
            }

            set
            {
                this._databaseName = value;
                this._rebuldForceFull = true;
            }
        }

        /// <summary>
        /// Gets or sets Appname for this application.
        /// </summary>
        public string Appname
        {
            get
            {
                return this._appnName;
            }

            set
            {
                this._appnName = value;
                this._rebuldForceFull = true;
            }
        }

        #endregion

        private string zzDebug { get; set; }

        private ConcurrentDictionary<string, ServerHelperNodeModel> Nodes { get; set; }

        /// <summary>
        /// Collect logs.
        /// </summary>
        public void CollectLogs()
        {
            _ = this.zzDebug;
        }

        /// <summary>
        /// Rebuild database connections.
        /// </summary>
        /// <param name="force">shod we force the rebuild.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task Rebuild(bool force)
        {
            if (this._rebuldForceFull || force)
            {
                // todo fix this.

                foreach (var node in this.Nodes)
                {
                    var tmpServer = node.Value.MdbClient;

                    // var connectionString = "mongodb://localhost";

                    //var client = new MongoClient(connectionString);
                    //var database = client.GetDatabase(this.DatabaseName);

                    var database = node.Value.MdbDatabase;

                    node.Value.StatusMgnRunning = true;

                    var hej = await database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
                    
                    //var hej1 = hej.Take(1);
                    node.Value.StatusMgnRunning = false;
                    // https://stackoverflow.com/questions/28835833/how-to-check-connection-to-mongodb

                    if (hej.ElementCount == 1)
                    {
                        foreach (var dd in hej.Elements)
                        {
                            if (dd.Name == "ok")
                            {
                                if (dd.Value.ToString() == "1")
                                {

                                }
                                this.zzDebug = "sddf";
                                
                            }
                            else
                            {
                                this.zzDebug = "sdfd";
                            }
                            
                        }
                    }
                    

                    this.zzDebug = "sdfdsf";
                   
                    
                    //if (!node.Value.InitIsDone || foreRebuild)
                    //{
                    //    node.Value.MdbConnectionString = this.NodeBuildConnectionString(node.Key);
                    //    this.zzDebug = "sfd";

                    //    node.Value.MdbClient = new MongoClient(node.Value.MdbConnectionString);
                    //    this.zzDebug = "22f";

                    //    node.Value.MdbDatabase = node.Value.MdbClient.GetDatabase(this.DatabaseName);
                    //    this.zzDebug = "sdfdf";

                    //    node.Value.InitIsDone = true;

                    //    // TODO do init of nodes that is note done.
                    //}
                }


            }

            this._rebuldForceFull = false;
        }

        /// <summary>
        /// Do node already exist?.
        /// </summary>
        /// <param name="name">name of node.</param>
        /// <returns>trur or false.</returns>
        public bool NodeExist(string name)
        {
            if (this.Nodes.ContainsKey(name))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Add new database server to nodes.
        /// </summary>
        /// <param name="data">ServerRegisterModel.</param>
        public void NodeAdd(ServerRegisterModel data)
        {
            if (data == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(data.Name))
            {
                return;
            }

            if (this.NodeExist(data.Name))
            {
                return;
            }

            if (string.IsNullOrEmpty(data.AuthSource) && !string.IsNullOrEmpty(this._databaseName))
            {
                data.AuthSource = this._databaseName;
            }

            this.Nodes.TryAdd(data.Name, new ServerHelperNodeModel()
            {
                RegData = data,
                InitIsDone = false,
            });

            this.Init();
        }

        /// <summary>
        /// Do init on all database connections.
        /// </summary>
        /// <param name="foreRebuild">Shod we force the database connection build.</param>
        public void Init(bool foreRebuild = false)
        {
            if (this.Nodes == null)
            {
                return;
            }

            if (this.Nodes.IsEmpty)
            {
                return;
            }

            foreach (var node in this.Nodes)
            {
                if (!node.Value.InitIsDone || foreRebuild)
                {
                    node.Value.MdbConnectionString = this.NodeBuildConnectionString(node.Key);
                    this.zzDebug = "sfd";

                    node.Value.MdbClient = new MongoClient(node.Value.MdbConnectionString);
                    this.zzDebug = "22f";

                    node.Value.MdbDatabase = node.Value.MdbClient.GetDatabase(this.DatabaseName);
                    this.zzDebug = "sdfdf";

                    node.Value.InitIsDone = true;

                    // TODO do init of nodes that is note done.
                }
            }

            if (this.Nodes.Count == 1)
            {
                if (string.IsNullOrEmpty(this._connectionString) || foreRebuild)
                {
                    this._connectionString = this.NodeBuildConnectionString();
                }

                if (string.IsNullOrEmpty(this._connectionStringWrite) || foreRebuild)
                {
                    this._connectionStringWrite = this.NodeBuildConnectionString();
                }
            }

            this.zzDebug = "sdfdf";
        }

        public IMongoDatabase GetDatabaseToUse()
        {
            var nodeToUseKey = this.Nodes.Keys.FirstOrDefault();
            return this.Nodes[nodeToUseKey].MdbDatabase;
        }

        private string NodeBuildConnectionString(string nodeName = null)
        {
            if (string.IsNullOrEmpty(nodeName))
            {
                // Nodename is null. Get first nodename from dict.
                nodeName = this.Nodes.Keys.First();
            }

            this.zzDebug = "sdfdf";
            if (string.IsNullOrEmpty(nodeName))
            {
                return string.Empty;
            }

            var tmpConData = this.Nodes[nodeName];

            this.zzDebug = "sdfd";

            // TODO Fix support for readpreference from node regdata.
            return $"mongodb://{tmpConData.RegData.Username}:{tmpConData.RegData.Password}@{tmpConData.RegData.Hostname}:{tmpConData.RegData.Port}/?authSource={tmpConData.RegData.AuthSource}&readPreference=primary&appname={this.Appname}&ssl={tmpConData.RegData.UseSsl.ToString().ToLower()}";
        }
    }
}
