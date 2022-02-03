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

    /// <summary>
    /// Server "node" helper.
    /// </summary>
    public class ServerHelper
    {
        private string _connectionString;
        private string _connectionStringWrite;
        private string _databaseName;

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
        }

        private string zzDebug { get; set; }

        private ConcurrentDictionary<string, ServerHelperNodeModel> Nodes { get; set; }

        private void Init(bool foreRebuild = false)
        {
            if (this.Nodes == null)
            {
                return;
            }

            if (this.Nodes.Count() == 0)
            {
                return;
            }

            if (this.Nodes.Count() == 1)
            {
                if (string.IsNullOrEmpty(this._connectionString) || foreRebuild)
                {
                    this._connectionString = this.NodeBuildConnectionString();
                    this._connectionStringWrite = this.NodeBuildConnectionString();
                }
            }
        }

        private string NodeBuildConnectionString(string nodeName = null)
        {
            if (nodeName == null)
            {
                // Nodename is null. Get first nodename from dict.
                nodeName = this.Nodes.Keys.First();
            }

            this.zzDebug = "sdfdf";

            return "";
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

            this.Nodes.TryAdd(data.Name, new ServerHelperNodeModel()
            {
                RegData = data,
            });

            this.Init();
        }
    }

    /// <summary>
    /// ServerHelperNodeModel.
    /// </summary>
    public class ServerHelperNodeModel
    {
        /// <summary>
        /// Gets or sets serverRegisterModel information.
        /// </summary>
        public ServerRegisterModel RegData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is init done on this server.
        /// </summary>
        public bool InitIsDone { get; set; }

        public ServerHelperNodeModel()
        {
            this.RegData = null;
            this.InitIsDone = false;

        }
    }
}
