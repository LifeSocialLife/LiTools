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
                                       "b. Back to encryption.",
                    },
                    new List<string>()
                   {
                                    "l",
                                    "c",
                                    "m",
                                    "x",
                                    "b",
                   });

                switch (selected)
                {
                    case "l":
                        await this.EncryptionRsaKeys_ListAll();
                        break;
                    case "b":
                        whileRun = false;
                        return;
                    case "c":
                        await this.EncryptionRsaKeys_CreateKeyOne();
                        break;
                    case "m":
                        await this.EncryptionRsaKeys_CreateMany(50, 2048);
                        break;
                    case "x":
                        await this.EncryptionRsaKeys_CreateMany(0, 0);
                        break;
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
                $"Create {count.ToString()} whit {bits.ToString()} bits encryption",
                "------------------------------------------------------------",
            });

            #region Input Count - How many keys to you want to create.

            if (count == 0)
            {
                // Enter how many keys shod be created.
                while (true)
                {
                    var tmpReturn = this._menu.DrawQuestion("How many do you want to create: ", false,false);

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
            });

            // Generate a storage name to use as base.
            var tmpStorageNameToUse = LiTools.Helpers.Generate.StringLines.RandomString(5, true,false,false);

            this.zzDebug = "sdfdsf";

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
