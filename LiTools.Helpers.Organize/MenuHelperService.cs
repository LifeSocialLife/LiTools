// <summary>
// MenuHelperService.
// </summary>
// <copyright file="MenuHelperService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Organize
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    //using Microsoft.Extensions.Logging;

    /// <summary>
    /// Menu service.
    /// </summary>
    public class MenuHelperService
    {
        private string zzDebug;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuHelperService"/> class.
        /// </summary>
        public MenuHelperService()
        {
            this.zzDebug = "MenuHelperService";
        }

        /// <summary>
        /// Output log to screen.
        /// </summary>
        public void OuputDebug()
        {
            Console.WriteLine(this.zzDebug);
        }

        /// <summary>
        /// Write output to screen.
        /// </summary>
        /// <param name="text">output text as string.</param>
        public void DrawTextLines(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Write output to screen.
        /// </summary>
        /// <param name="items">output text as list.</param>
        public void DrawTextLines(List<string> items)
        {
            foreach (var item in items)
            {
                this.DrawTextLines(item);
            }
        }

        /// <summary>
        /// Ask question.
        /// </summary>
        /// <param name="text">question.</param>
        /// <param name="answerOnNewLine">new line for input text.</param>
        /// <param name="allowEmptyAnswer">allow empty answer as return.</param>
        /// <returns>answer.</returns>
        public string DrawQuestion(string text, bool answerOnNewLine = false, bool allowEmptyAnswer = true)
        {
            while (true)
            {
                if (answerOnNewLine)
                {
                    Console.Write(text);
                }
                else
                {
                    Console.WriteLine(text);
                }

                var tmpAnswer = Console.ReadLine();

                if (allowEmptyAnswer)
                {
                    return tmpAnswer;
                }

                if (!string.IsNullOrEmpty(tmpAnswer))
                {
                    return tmpAnswer;
                }

                Console.WriteLine(" ");
            }
        }

        /// <summary>
        /// Show menu options on screen.
        /// </summary>
        /// <param name="items">Menu items to select from.</param>
        /// <param name="keys">Allow key to return. if null return all.</param>
        /// <returns>selected item as string.</returns>
        public string DrawMenuSelectList(List<string> items, List<string>? keys = null)
        {
            string tmpReturn;

            while (true)
            {
                this.DrawTextLines(items);

                Console.WriteLine("==-- Select --==");

                // var key = Console.ReadKey();
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                Console.WriteLine(" ");

                tmpReturn = keyInfo.KeyChar.ToString();

                this.zzDebug = "dsfdf";

                if (string.IsNullOrEmpty(tmpReturn))
                {
                    continue;
                }

                if (keys == null)
                {
                    break;
                }

                if (keys.Count == 0)
                {
                    break;
                }

                if (keys.Contains(tmpReturn, StringComparer.OrdinalIgnoreCase))
                {
                    break;
                }

                // Console.WriteLine(" ");
                Console.WriteLine("------------------");
                Console.WriteLine("Key not allow");
                Console.WriteLine("------------------");
                Console.WriteLine("------------------");
            }

            return tmpReturn;
        }

        /// <summary>
        /// Draw Press key to continue.
        /// </summary>
        /// <param name="extraText">extra string output.</param>
        public void DrawPressKeyToContinue(string extraText = "")
        {
            var tmpText = new List<string>();
            tmpText.Add("---------------------------------------");
            if (!string.IsNullOrEmpty(extraText))
            {
                tmpText.Add(extraText);
            }

            tmpText.Add("Press any key to continue.");

            this.DrawMenuSelectList(tmpText, null);

            this.DrawTextLines(new List<string>() { " ", });
        }
    }
}
