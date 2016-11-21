﻿// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels
{
    using System;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public class ConnectionViewModel : WPF.ViewModels.ConnectionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="buildProvider">The build provider.</param>
        public ConnectionViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IBuildProvider buildProvider)
            : base(eventAggregator, buildMonitor, buildProvider)
        {
        }

        /// <summary>
        /// Gets the build provider name.
        /// </summary>
        /// <value>
        /// The build provider name.
        /// </value>
        public override string BuildProviderName { get; } = "Visual Studio Team Services";

        /// <summary>
        /// Gets the type of the view model to edit the connection.
        /// </summary>
        /// <value>
        /// The type of the view model to edit the connection.
        /// </value>
        protected override Type EditConnectionViewModelType { get; } = typeof(EditConnectionViewModel);
    }
}