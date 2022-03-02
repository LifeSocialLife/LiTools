// <summary>
// RSA Crypto service - RsaCryptoDictonaryModel.
// </summary>
// <copyright file="RsaCryptoDictonaryModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Encoding.Cryptography
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// RsaCryptoDictonaryModel.
    /// </summary>
    public class RsaCryptoDictonaryModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RsaCryptoDictonaryModel"/> class.
        /// </summary>
        public RsaCryptoDictonaryModel()
        {
            this.Csp = new RSACryptoServiceProvider(8);
            this.KeyExistPublic = false;
            this.KeyExistPrivate = false;
            this.DtCreated = DateTime.UtcNow;
            this.DtLastUsed = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets rsa crypto service provider. this can be null. always check.
        /// </summary>
        public RSACryptoServiceProvider Csp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether do we have private key?.
        /// </summary>
        public bool KeyExistPrivate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether do we have public key?.
        /// </summary>
        public bool KeyExistPublic { get; set; }

        /// <summary>
        /// Gets or sets when was this created.
        /// </summary>
        public DateTime DtCreated { get; set; }

        /// <summary>
        /// Gets or sets when was this last used.
        /// </summary>
        public DateTime DtLastUsed { get; set; }

        /*
        public RSAParameters PubKey { get; set; }
        public RSAParameters PrivKey { get; set; }
        */
    }
}
