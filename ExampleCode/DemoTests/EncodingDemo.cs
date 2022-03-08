// <summary>
// EncodingDemo.
// </summary>
// <copyright file="EncodingDemo.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace ExampleCode.DemoTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LiTools.Helpers.Encoding.Cryptography;
    using LiTools.Helpers.Organize;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Encoding Demo.
    /// </summary>
    public class EncodingDemo
    {
        private readonly ILogger<EncodingDemo> _logger;

        private readonly MenuHelperService _menu;
        private readonly RsaCryptoService rsaCrypto;

        private string zzDebug;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodingDemo"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="rsaCryptoService">RsaCryptoService.</param>
        /// <param name="menu">MenuHelperService.</param>
        public EncodingDemo(ILogger<EncodingDemo> logger, RsaCryptoService rsaCryptoService, MenuHelperService menu)
        {
            this.zzDebug = "Worker";
            this._logger = logger;
            this.rsaCrypto = rsaCryptoService;
            this._menu = menu;
        }

        /// <summary>
        /// Main menu.
        /// </summary>
        /// <returns>When done.</returns>
        public async Task Menu()
        {
            var selected = this._menu.DrawMenuSelectList(
                new List<string>()
            {
                "0. Back to main menu.",
                "1. RSA crytography",
            },
                new List<string>()
                {
                    "0",
                    "1",
                });

            switch (selected)
            {
                case "0":
                    return;
                case "1":
                    await this.EncryptionRsaMenu();
                    break;
            }

            this.zzDebug = "sdfd";
        }

        #region RSA crypto service.

        private async Task EncryptionRsaMenu()
        {
            bool whileRun = true;
            while (whileRun)
            {
                var selected = this._menu.DrawMenuSelectList(
                    new List<string>()
                {
                    "======================",
                    "-- Encryption - RSA --",
                    "----------------------",
                    "k. Keys",

                    "b. Back to main menu.",
                },
                    new List<string>()
                   {
                                    "k",
                                    "b",
                   });

                switch (selected)
                {
                    case "b":
                        whileRun = false;
                        return;
                    case "k":
                        await this.EncryptionRsaKeysMenu();
                        break;
                }
            }

            this.zzDebug = "sdfdf";
        }

        private async Task EncryptionRsaKeysMenu()
        {
            bool whileRun = true;
            while (whileRun)
            {
                var selected = this._menu.DrawMenuSelectList(
                    new List<string>()
                    {
                                       "===========================",
                                       "-- Encryption RSA - Keys --",
                                       "---------------------------",
                                       "l. List all keys loaded",
                                       "c. Create new key",
                                       "m. Create 50 keys whit 2048bits encryption.",
                                       "x. Create X keys whit X bits encryption.",
                                       "d. Get private and public key from default key.",
                                       "b. Back to encryption.",
                    },
                    new List<string>()
                   {
                                    "l",
                                    "c",
                                    "m",
                                    "x",
                                    "d",
                                    "b",
                   });

                switch (selected)
                {
                    case "l":
                        await this.EncryptionRsaKeys_ListAll();
                        break;
                    case "c":
                        await this.EncryptionRsaKeys_CreateKeyOne();
                        break;
                    case "m":
                        await this.EncryptionRsaKeys_CreateMany(50, 2048);
                        break;
                    case "x":
                        await this.EncryptionRsaKeys_CreateMany(0, 0);
                        break;
                    case "d":
                        await this.EncryptionRSAKeys_GetKeys("default");
                        break;
                    case "b":
                        whileRun = false;
                        return;
                }
            }

            this.zzDebug = "sdfdf";
        }

        private async Task EncryptionRsaKeys_ListAll()
        {
            this._menu.DrawTextLines(new List<string>()
            {
                "----------------------",
                "-- Keys in storage  --",
                "----------------------",
            });

            var list = this.rsaCrypto.GetKeysInStorage(KeyTypesInStorage.All);

            if (list.Count == 0)
            {
                this._menu.DrawTextLines(new List<string>()
                {
                    "No keys exist in storage !!",
                    "---------------------------",
                });
            }
            else
            {
                var tmpOutputStorageInformation = new List<string>();

                tmpOutputStorageInformation.Add($"Keys count: {list.Count.ToString()}");
                tmpOutputStorageInformation.Add("--------------------------------------------------------");
                tmpOutputStorageInformation.Add("Name                          | Private | Public  | Size");
                tmpOutputStorageInformation.Add("--------------------------------------------------------");

                foreach (var key in list)
                {
                    string lineOutput = string.Empty;

                    lineOutput = key.Name;
                    if (key.Name.Length < 30)
                    {
                        for (int i = 0; i < (30 - key.Name.Length); i++)
                        {
                            lineOutput += " ";
                        }
                    }

                    lineOutput += $"| {key.KeyPrivate.ToString().ToLower()}    | {key.KeyPublic.ToString().ToLower()}    | {key.KeySize}";

                    tmpOutputStorageInformation.Add(lineOutput);

                    this.zzDebug = "sdfdf";
                }

                tmpOutputStorageInformation.Add("--------------------------------------------------------");
                tmpOutputStorageInformation.Add("Name                          | Private | Public  | Size");
                tmpOutputStorageInformation.Add("--------------------------------------------------------");

                this._menu.DrawTextLines(tmpOutputStorageInformation);
            }

            this._menu.DrawPressKeyToContinue();

            this.zzDebug = "sdfsdfd";
            await Task.Delay(1);
        }

        private async Task EncryptionRsaKeys_CreateKeyOne()
        {
            this._menu.DrawTextLines(new List<string>()
            {
                "---------------------",
                "-- Create one key  --",
                "---------------------",
            });

            string stgName = string.Empty;

            bool whileRun = true;

            while (whileRun)
            {
                this._menu.DrawTextLines(new List<string>()
                {
                    "Enter storage name where you want to save the key.",
                    "Leve empty to save in default.",
                    "Type quit to return.",
                    "---------------------",
                });

                stgName = this._menu.DrawQuestion("Storage name: ", false, true);

                this._menu.DrawTextLines(new List<string>()
                        {
                            " ",
                        });

                if (string.IsNullOrEmpty(stgName))
                {
                    stgName = "default";
                }

                stgName = stgName.ToLower().Trim();

                if (stgName == "quit")
                {
                    return;
                }

                // Check if storagename already exist.
                if (this.rsaCrypto.StorageExist(stgName))
                {
                    this._menu.DrawTextLines(new List<string>()
                    {
                        "Storage name already exist. Select new name.",
                        "--------------------------------------------",
                    });
                    continue;
                }

                // Shod we create whit this name.
                var selected = this._menu.DrawMenuSelectList(
                   new List<string>()
                        {
                        "---------------------------",
                        "Do you want to create this storage?",
                        $"Name: {stgName}",
                        "(y)es or (n)o",
                        },
                   new List<string>()
                       {
                                    "y",
                                    "n",
                       });

                this._menu.DrawTextLines(new List<string>()
                        {
                            " ",
                        });

                switch (selected)
                {
                    case "y":
                        whileRun = false;
                        break;
                    default:
                        break;
                }
            }

            this.zzDebug = "sdfdf";

            this._menu.DrawTextLines(new List<string>()
                        {
                            "Generating 4096 bits key.",
                        });

            var keyResault = await this.rsaCrypto.GenerateKey(stgName, 4096);

            string tmpText = string.Empty;

            if (keyResault)
            {
                tmpText = "Key is created.";
            }
            else
            {
                tmpText = "Error creating key. See log output.";
            }

            this._menu.DrawPressKeyToContinue(tmpText);

            this.zzDebug = "sdfd";

            await Task.Delay(1);
        }

        private async Task EncryptionRsaKeys_CreateMany(int count = 0, int bits = 0)
        {
            this._menu.DrawTextLines(new List<string>()
            {
                "---------------------",
                "-- Create many key --",
                "---------------------",
            });

            // Allow maxe 999 item to be created.
            if (count > 200)
            {
                count = 0;
            }

            #region Input Count - How many keys to you want to create.

            if (count == 0)
            {
                // Enter how many keys shod be created.
                while (true)
                {
                    var tmpReturn = this._menu.DrawQuestion("How many do you want to create: ", false, false);

                    if (!LiTools.Helpers.Check.Strings.ContainsOnlyNumbers(tmpReturn))
                    {
                        this._menu.DrawTextLines("Enter only numbers !!!");
                        continue;
                    }

                    if (int.TryParse(tmpReturn, out int j))
                    {
                        if (j == 0)
                        {
                            this._menu.DrawPressKeyToContinue("Return to menu.");
                            return;
                        }

                        if (j > 200)
                        {
                            this._menu.DrawTextLines("You can max create 200 keys !!");
                            continue;
                        }

                        count = j;
                        break;
                    }

                    this._menu.DrawTextLines("Something went wrong.");
                }
            }

            #endregion

            #region Key size

            if (bits == 0)
            {
                this._menu.DrawTextLines("If you leave this empty, key size will be 2048 bits on every key created.");

                while (true)
                {
                    var tmpReturn = this._menu.DrawQuestion("How many bits shod every key size be: ", false, true);

                    if (string.IsNullOrEmpty(tmpReturn))
                    {
                        tmpReturn = "2048";
                    }

                    if (!LiTools.Helpers.Check.Strings.ContainsOnlyNumbers(tmpReturn))
                    {
                        this._menu.DrawTextLines("Enter only numbers !!!");
                        continue;
                    }

                    if (int.TryParse(tmpReturn, out int j))
                    {
                        if (j == 0)
                        {
                            this._menu.DrawTextLines("Using key size 2048 bits.");
                            bits = 2048;
                        }
                        else if (j < 384)
                        {
                            this._menu.DrawTextLines("Using key size 384 bits.");
                            bits = 384;
                        }
                        else if (j > 16384)
                        {
                            bits = 16384;
                            this._menu.DrawTextLines("Using key size 16384 bits.");
                        }
                        else
                        {
                            bits = j;
                        }
                    }

                    if (LiTools.Helpers.Check.NumberChecks.PowerOf(bits, 8))
                    {
                        this.zzDebug = "sdfdsf";
                        break;
                    }
                    else
                    {
                        this._menu.DrawTextLines("Keysize need to be in increments of 8 bits.");
                    }

                    this.zzDebug = "dsfdsf";
                }
            }

            #endregion

            if (!LiTools.Helpers.Check.NumberChecks.PowerOf(bits, 8))
            {
                this._menu.DrawTextLines("Keysize need to be in increments of 8 bits.");
                return;
            }

            this._menu.DrawTextLines(new List<string>()
            {
                "------------------------------------------------------------",
                $"Create {count.ToString()} keys, whit {bits.ToString()} bits encryption",
                "------------------------------------------------------------",
                "------------------------------------------------------------",
            });

            string tmpStorageNameToUse = string.Empty;

            #region Generate storagename and check that is not already in use.

            while (true)
            {

                // Generate a storage name to use as base.
                tmpStorageNameToUse = LiTools.Helpers.Generate.StringLines.RandomString(5, true, false, false);

                // Check if it alread is in use.
                if (!this.rsaCrypto.StorageExist(tmpStorageNameToUse + "001"))
                {
                    break;
                }

                await Task.Delay(1000);
            }

            #endregion

            int countDone = 0;
            int countFaild = 0;

            #region Create keys.

            for (int i = 1; i <= count; i++)
            {
                string tmpStgName = tmpStorageNameToUse;

                if (i.ToString().Length == 2)
                {
                    tmpStgName += "0";
                }
                else if (i.ToString().Length == 1)
                {
                    tmpStgName += "00";
                }

                tmpStgName += i.ToString();

                this.zzDebug = "SDfdf";

                if (await this.rsaCrypto.GenerateKey(tmpStgName, bits))
                {
                    countDone++;
                }
                else
                {
                    countFaild++;
                }

                this._menu.DrawTextLines(new List<string>()
                {
                    $"{i} of {count} done. {countDone} successful and {countFaild} failed.",
                });

                await Task.Delay(100);

            }

            #endregion

            this.zzDebug = "sdfdsf";

            this._menu.DrawPressKeyToContinue();

            await Task.Delay(1);
        }

        private async Task EncryptionRSAKeys_GetKeys(string storageId)
        {
            this._menu.DrawTextLines(new List<string>()
            {
                "==============================================",
                "--------------",
                "-- Get keys --",
                "--------------",
                $"storage: {storageId}",
            });

            // check if storage exist.
            while (true)
            {
                if (this.rsaCrypto.StorageExist(storageId))
                {
                    // storage exist.
                    break;
                }

                // Dont exist. do we want to create it.
                var selected = this._menu.DrawMenuSelectList(
                    new List<string>()
                    {
                                       "Storage don’t exist. Do you want to create it?",
                                       "---------------------------",
                                       "c. Create storage.",
                                       "a. Abort, return to menu.",
                    },
                    new List<string>()
                   {
                                    "c",
                                    "a",
                   });

                if (selected == "a")
                {
                    return;
                }
                else if (selected == "c")
                {
                    this._menu.DrawTextLines(new List<string>()
                    {
                        "Creating new key.",
                    });

                    if (await this.rsaCrypto.GenerateKey(storageId, 2048))
                    {
                        break;
                    }

                    this._menu.DrawTextLines(new List<string>()
                    {
                        "Error creating key.",
                    });
                    this._menu.DrawPressKeyToContinue();
                }

                this.zzDebug = "dsfd";
            }

            this.zzDebug = "sdfdf";

            // Get private key if it exist.
            var tmpPrivateKey = this.rsaCrypto.GetKeyPrivateAsString(storageId);
            var tmpPublicKey = this.rsaCrypto.GetKeyPublicAsString(storageId);

            this.zzDebug = "sdfdf";

            this._menu.DrawPressKeyToContinue();

            await Task.Delay(1);
        }

        #endregion

        private void OuputDebug()
        {
            this._menu.DrawTextLines(this.zzDebug);
        }
    }
}
