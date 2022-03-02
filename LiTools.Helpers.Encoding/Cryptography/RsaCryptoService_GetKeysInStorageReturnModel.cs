// <summary>
// RSA Crypto service - RsaCryptoService_GetKeysInStorageReturnModel
// </summary>
// <copyright file="RsaCryptoService_GetKeysInStorageReturnModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Encoding.Cryptography
{
    /// <summary>
    /// Get keys in storage return model.
    /// </summary>
    public class RsaCryptoService_GetKeysInStorageReturnModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RsaCryptoService_GetKeysInStorageReturnModel"/> class.
        /// </summary>
        public RsaCryptoService_GetKeysInStorageReturnModel()
        {
            this.Name = string.Empty;
            this.KeyPrivate = false;
            this.KeyPublic = false;
            this.KeySize = string.Empty;
        }

        /// <summary>
        /// Gets or sets name of key in storage.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether key private exist.
        /// </summary>
        public bool KeyPrivate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether key Public exist.
        /// </summary>
        public bool KeyPublic { get; set; }

        /// <summary>
        /// Gets or sets key size.
        /// </summary>
        public string KeySize { get; set; }
    }
}
