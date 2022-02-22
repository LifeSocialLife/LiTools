// <summary>
// Generate css style strings.
// </summary>
// <copyright file="CssTextColorOptionsHelper.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.Generate
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// options that can be used inside style.
    /// </summary>
    public enum CssColorTypesEnum
    {
        /// <summary>Text color.</summary>
        Color,

        /// <summary>Border color.</summary>
        Border_Color,

        /// <summary>Background color.</summary>
        Background_Color,
    }

    /// <summary>
    /// Css color to select.
    /// </summary>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum CssColorsEnum
    {
        NotSet,
        Grey,
        Lightgrey,
        Red,
        Green,
        Blue,
        Pink,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Css style helper to build string.
    /// </summary>
    public class CssTextColorOptionsHelper
    {
        /*
         * https://www.codeply.com/go/bp/RkhHZbahVE
         *
         * https://www.w3schools.com/css/css_border_color.asp
         * https://www.w3schools.com/cssref/pr_border-color.asp
         * https://www.w3schools.com/cssref/css_colors.asp
         *
         * https://docs.microsoft.com/en-us/dotnet/api/system.drawing.color?view=net-6.0
         *
         * */

        /// <summary>
        /// Initializes a new instance of the <see cref="CssTextColorOptionsHelper"/> class.
        /// </summary>
        public CssTextColorOptionsHelper()
        {
            this.Options = new Dictionary<CssColorTypesEnum, CssColorsEnum>();
            this.StyleExtraData = string.Empty;
        }

        /// <summary>
        /// Gets all style as a string.
        /// </summary>
        public string ColorAsStyleString
        {
            get
            {
                string tmpReturn = string.Empty;
                if (this.Options?.Count > 0)
                {
                    foreach (var item in this.Options)
                    {
                        if (item.Value == CssColorsEnum.NotSet)
                        {
                            continue;
                        }

                        tmpReturn += $"{item.Key.ToString().ToLower().Replace('_', '-')}: {item.Value.ToString().ToLower()};";
                    }
                }

                if (!string.IsNullOrEmpty(this.StyleExtraData))
                {
                    tmpReturn = this.StyleExtraData + " " + tmpReturn;
                }

                return tmpReturn;
            }
        }

        /// <summary>
        /// Gets or sets color - Border.
        /// </summary>
        public CssColorsEnum ColorBorder
        {
            get
            {
                if (this.Options.ContainsKey(CssColorTypesEnum.Border_Color))
                {
                    return this.Options[CssColorTypesEnum.Border_Color];
                }

                return CssColorsEnum.NotSet;
            }

            set
            {
                if (this.Options.ContainsKey(CssColorTypesEnum.Border_Color))
                {
                    if (value == CssColorsEnum.NotSet)
                    {
                        this.Options.Remove(CssColorTypesEnum.Border_Color);
                    }
                    else
                    {
                        this.Options[CssColorTypesEnum.Border_Color] = value;
                    }
                }
                else
                {
                    if (value == CssColorsEnum.NotSet)
                    {
                        return;
                    }

                    this.Options.Add(CssColorTypesEnum.Border_Color, value);
                }
            }
        }

        /// <summary>
        /// Sets - Dont use this.
        /// </summary>
        public CssColorsEnum SetTextColor
        {
            set
            {
                if (this.Options.ContainsKey(CssColorTypesEnum.Color))
                {
                    this.Options[CssColorTypesEnum.Color] = value;
                }
                else
                {
                    this.Options.Add(CssColorTypesEnum.Color, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets extra information that this style string shod have.
        /// </summary>
        public string StyleExtraData { get; set; }

        private Dictionary<CssColorTypesEnum, CssColorsEnum> Options { get; set; }

        /// <summary>
        /// Set color to an styel type.
        /// </summary>
        /// <param name="cssType">css style type.</param>
        /// <param name="cssColor">color to set.</param>
        public void SetColor(CssColorTypesEnum cssType, CssColorsEnum cssColor)
        {
            if (this.Options.ContainsKey(cssType))
            {
                this.Options[cssType] = cssColor;
            }
            else
            {
                this.Options.Add(cssType, cssColor);
            }
        }
    }
}
