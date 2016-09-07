﻿// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public class ConnectionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="settings" /> is <c>null</c>.</exception>
        public ConnectionViewModel(BuildProviderSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            ProviderName = BuildProviderFactory.GetProvider(settings).ProviderName;
            ConnectionName = settings.Name;


            ProjectBuildViewModels = new List<ProjectBuildViewModel>()
            {
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
            };

        }

        /// <summary>
        /// Gets the connection name.
        /// </summary>
        /// <value>
        /// The connection name.
        /// </value>
        public string ConnectionName { get; }

        /// <summary>
        /// Gets provider name.
        /// </summary>
        /// <value>
        /// The provider name.
        /// </value>
        public string ProviderName { get; }

        public IEnumerable<ProjectBuildViewModel> ProjectBuildViewModels { get; set; }
    }
}