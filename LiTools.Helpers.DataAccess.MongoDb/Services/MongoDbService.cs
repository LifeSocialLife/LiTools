// <summary>
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
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbService"/> class.
        /// </summary>
        public MongoDbService()
        {
            this.zzDebug = "MongoDbService";
            this.Servers = new ServerHelper();
        }

        private ServerHelper Servers { get; set; }

        private string zzDebug { get; set; }

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

            this.Servers.NodeAdd(data);
        }

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
