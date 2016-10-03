﻿// <copyright file="BuildProviderViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProviderViewModel" /> class.
    /// </summary>
    public abstract class BuildProviderViewModel : PropertyChangedBase
    {
        private readonly IBuildProvider _buildProvider;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProvider">The build provider.</param>
        protected BuildProviderViewModel(IEventAggregator eventAggregator, IBuildProvider buildProvider)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildProvider).IsNotNull();

            _eventAggregator = eventAggregator;
            _buildProvider = buildProvider;
        }

        /// <summary>
        /// Gets build provider name.
        /// </summary>
        /// <value>
        /// The build provider name.
        /// </value>
        public string BuildProviderName => _buildProvider.Name;

        /// <summary>
        /// Gets the type of the view model to add connection.
        /// </summary>
        /// <value>
        /// The type of the view model to add connection.
        /// </value>
        protected abstract Type AddConnectionViewModel { get; }

        /// <summary>
        /// Navigates to the view to add connection.
        /// </summary>
        public void AddConnection()
        {
            var message = new NavigationMessage(AddConnectionViewModel);

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}