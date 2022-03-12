// <summary>
// RSA Crypto service.
// </summary>
// <copyright file="RsaCryptoService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Encoding.Cryptography
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Key types that exists.
    /// </summary>
    public enum KeyTypesInStorage
    {
        /// <summary>All key types.</summary>
        All,

        /// <summary>private key types.</summary>
        Private,

        /// <summary>public key types.</summary>
        Public,
    }

    /// <summary>
    /// Key Save to File return status.
    /// </summary>
    public enum KeySaveToFileReturnEnum
    {
        /// <summary>File is saved.</summary>
        Saved,

        /// <summary>Error when saving file.</summary>
        ErrorSavingFile,

        /// <summary>Filename alrady exist.</summary>
        ErrorFileAlreadyExist,

        /// <summary>File folder dont exist.</summary>
        ErrorDirectoryDontExist,

        /// <summary>Encryption key to exist in storage.</summary>
        KeyDontExist,

        /// <summary>call input data error.</summary>
        ErrorInput,
    }

    /// <summary>
    /// RSA Crypto service.
    /// </summary>
    public class RsaCryptoService
    {
        private string zzDebug;

        private ConcurrentDictionary<string, RsaCryptoDictonaryModel> storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaCryptoService"/> class.
        /// </summary>
        public RsaCryptoService()
        {
            this.zzDebug = "RsaCryptoService";
            this.storage = new ConcurrentDictionary<string, RsaCryptoDictonaryModel>();
        }

        /// <summary>
        /// Only for Dev and Testing.
        /// </summary>
        /// <returns>zz Debug.</returns>
        public string GetLastzzDebug()
        {
            return this.zzDebug;
        }

        /// <summary>
        /// Do storage exist.
        /// </summary>
        /// <param name="storage">storage name.</param>
        /// <returns>true or false.</returns>
        public bool StorageExist(string storage)
        {
            if (this.storage.ContainsKey(storage))
            {
                return true;
            }

            return false;
        }

        #region Keys

        /// <summary>
        /// Get keys in storage.
        /// </summary>
        /// <param name="typeToGet">Key type to get.</param>
        /// <returns>RsaCryptoService_GetKeysInStorageReturnModel.</returns>
        public List<RsaCryptoService_GetKeysInStorageReturnModel> GetKeysInStorage(KeyTypesInStorage typeToGet)
        {
            var tmpReturn = new List<RsaCryptoService_GetKeysInStorageReturnModel>();

            if (this.storage.Count == 0)
            {
                return tmpReturn;
            }

            foreach (var st in this.storage)
            {
                bool getThis = false;

                switch (typeToGet)
                {
                    case KeyTypesInStorage.All:
                        getThis = true;
                        break;
                    case KeyTypesInStorage.Private:
                        if (st.Value.KeyExistPrivate)
                        {
                            getThis = true;
                        }

                        break;

                    case KeyTypesInStorage.Public:
                        if (st.Value.KeyExistPublic)
                        {
                            getThis |= true;
                        }

                        break;

                    default:
                        if (Debugger.IsAttached)
                        {
                            Debugger.Break();
                        }

                        getThis = true;
                        break;
                }

                if (getThis)
                {
                    tmpReturn.Add(new RsaCryptoService_GetKeysInStorageReturnModel()
                    {
                        Name = st.Key,
                        KeyPrivate = st.Value.KeyExistPrivate,
                        KeyPublic = st.Value.KeyExistPublic,
                        KeySize = st.Value.Csp.KeySize.ToString(),
                    });
                }
            }

            return tmpReturn;
        }

        /// <summary>
        /// Generate rsa crypto keys.
        /// </summary>
        /// <param name="storage">Storage name where the key shod be saved. If null it will be saved in default.</param>
        /// <param name="keysize">Size of the key. Default is 2048 bits.</param>
        /// <returns>true or false.</returns>
        public async Task<bool> GenerateKey(string storage = "default", int keysize = 2048)
        {
            #region Storage exist or not create storage.

            if (this.storage.ContainsKey(storage))
            {
                return false;
            }

            try
            {
                this.storage.TryAdd(storage, new RsaCryptoDictonaryModel());
            }
            catch (OverflowException ex)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                this.zzDebug = ex.Message;
                return false;
            }
            catch (ArgumentNullException ex)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                this.zzDebug = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                this.zzDebug = ex.Message;
                return false;
            }

            #endregion

            try
            {
                // lets take a new CSP with a new keysize bit rsa key pair
                this.storage[storage].Csp = new RSACryptoServiceProvider(keysize);
                this.storage[storage].KeyExistPrivate = true;
                this.storage[storage].KeyExistPublic = true;

                this.zzDebug = "sdfdf";
            }
            catch (CryptographicException e)
            {
                if (Debugger.IsAttached)
                {
                    this.zzDebug = e.Message.ToString();
                    Debugger.Break();
                }

                this.zzDebug = "sdfdf";
                return false;
            }
            catch
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                return false;
            }

            await Task.Delay(1);
            return true;
        }

        /// <summary>
        /// Get size of key.
        /// </summary>
        /// <param name="storage">Stoage id.</param>
        /// <returns>Key size as int.</returns>
        public int GetKeySize(string storage)
        {
            int tmpReturn = 0;

            if (this.StorageExist(storage))
            {
                if (this.storage[storage].Csp == null)
                {
                    return tmpReturn;
                }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                tmpReturn = this.storage[storage].Csp.KeySize;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            return tmpReturn;
        }

        /// <summary>
        /// Get public key as RSA parameters.
        /// </summary>
        /// <param name="storage">storage id.</param>
        /// <returns>RSAParameters.</returns>
        public RSAParameters? GetKeyPublicAsRsaParameter(string storage)
        {
            if (!this.StorageExist(storage))
            {
                return null;
            }

            if (this.storage[storage].Csp == null)
            {
                return null;
            }

            if (this.storage[storage].KeyExistPublic)
            {
                return this.storage[storage].Csp?.ExportParameters(false);
            }

            return null;
        }

        /// <summary>
        /// Get private key as RSA parameters.
        /// </summary>
        /// <param name="storage">storage id.</param>
        /// <returns>RSAParameters.</returns>
        public RSAParameters? GetKeyPrivateAsRsaParameter(string storage)
        {
            if (!this.StorageExist(storage))
            {
                return null;
            }

            if (this.storage[storage].Csp == null)
            {
                return null;
            }

            if (this.storage[storage].KeyExistPrivate)
            {
                return this.storage[storage].Csp?.ExportParameters(true);
            }

            return null;
        }

        /// <summary>
        /// Get private key as string.
        /// </summary>
        /// <param name="storage">storage id.</param>
        /// <returns>key as string.</returns>
        public string GetKeyPrivateAsString(string storage)
        {
            var tmpData = this.GetKeyPrivateAsRsaParameter(storage);

            if (tmpData == null)
            {
                return string.Empty;
            }

            return this.ParameterIntoString((RSAParameters)tmpData);
        }

        /// <summary>
        /// Get public key as string.
        /// </summary>
        /// <param name="storage">storage id.</param>
        /// <returns>key as string.</returns>
        public string GetKeyPublicAsString(string storage)
        {
            var tmpData = this.GetKeyPublicAsRsaParameter(storage);

            if (tmpData == null)
            {
                return string.Empty;
            }

            return this.ParameterIntoString((RSAParameters)tmpData);
        }

        /// <summary>
        /// Save a RSA crypto key to a file.
        /// </summary>
        /// <param name="keyid">Storage id where the key is stored.</param>
        /// <param name="pathAndFileName">Filename to where it shod be saved.</param>
        /// <param name="keyToSave">KeyTypesInStorage - Key to save.</param>
        /// <returns>enum KeySaveToFileReturnEnum.</returns>
        public async Task<KeySaveToFileReturnEnum> KeySaveToFile(string keyid, string pathAndFileName, KeyTypesInStorage keyToSave = KeyTypesInStorage.Private)
        {
            // Check starting variables.
            if (keyToSave != KeyTypesInStorage.Private && keyToSave != KeyTypesInStorage.Public)
            {
                // Dont know key to save.
                return KeySaveToFileReturnEnum.ErrorInput;
            }

            if (string.IsNullOrEmpty(keyid) || string.IsNullOrEmpty(pathAndFileName))
            {
                return KeySaveToFileReturnEnum.ErrorInput;
            }

            // Check if storageid exist. (keyId).
            if (this.StorageExist(keyid))
            {
                return KeySaveToFileReturnEnum.KeyDontExist;
            }

            // Get folder from filepath
            string tmpFolder = LiTools.Helpers.IO.File.GetFolderFromFilePath(pathAndFileName);
            if (string.IsNullOrEmpty(tmpFolder))
            {
                return KeySaveToFileReturnEnum.ErrorInput;
            }

            // Check if directory exist.
            if (!LiTools.Helpers.IO.Directory.Exist(tmpFolder))
            {
                // Dont exist. return error.
                return KeySaveToFileReturnEnum.ErrorDirectoryDontExist;
            }

            if (LiTools.Helpers.IO.File.Exist(pathAndFileName))
            {
                return KeySaveToFileReturnEnum.ErrorFileAlreadyExist;
            }

            // Get Key as string that we shod save to file.
            string tmpKeyData = string.Empty;
            if (keyToSave == KeyTypesInStorage.Private)
            {
                tmpKeyData = this.GetKeyPrivateAsString(keyid);
            }
            else if (keyToSave == KeyTypesInStorage.Public)
            {
                tmpKeyData = this.GetKeyPublicAsString(keyid);
            }

            if (string.IsNullOrEmpty(tmpKeyData))
            {
                return KeySaveToFileReturnEnum.KeyDontExist;
            }

            // Save key to file.
            if (!LiTools.Helpers.IO.File.WriteFile(pathAndFileName, tmpKeyData, false))
            {
                // Error saving file.
                return KeySaveToFileReturnEnum.ErrorSavingFile;
            }

            /*
            // https://www.delftstack.com/howto/csharp/csharp-get-executable-path/
            string execPath = AppDomain.CurrentDomain.BaseDirectory;
            */

            this.zzDebug = "sdfdsf";
            await Task.Delay(1);
            return KeySaveToFileReturnEnum.Saved;
        }

        #endregion

        private string ParameterIntoString(RSAParameters par)
        {
            // we need some buffer
            var sw = new StringWriter();

            // we need a serializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

            // serialize the key into the stream
            xs.Serialize(sw, par);

            this.zzDebug = "dsfds";

            // get the string from the stream
            string pubKeyString = sw.ToString();

            if (string.IsNullOrEmpty(pubKeyString))
            {
                return string.Empty;
            }

            return pubKeyString;
        }
    }
}
