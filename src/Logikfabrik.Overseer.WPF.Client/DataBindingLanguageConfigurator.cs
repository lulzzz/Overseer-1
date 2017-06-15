﻿// <copyright file="DataBindingLanguageConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Markup;

    /// <summary>
    /// The <see cref="DataBindingLanguageConfigurator" /> class.
    /// </summary>
    public static class DataBindingLanguageConfigurator
    {
        /// <summary>
        /// Configures the data binding language.
        /// </summary>
        public static void Configure()
        {
            var language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);

            FrameworkContentElement.LanguageProperty.OverrideMetadata(typeof(TextElement), new FrameworkPropertyMetadata(language));
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(language));
        }
    }
}
