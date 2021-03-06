﻿// <copyright file="BuildProviderViewModel{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProviderViewModel{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1">The <see cref="ConnectionSettings" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="EditConnectionSettingsViewModel{T}" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildProviderViewModel<T1, T2> : PropertyChangedBase, IBuildProviderViewModel
        where T1 : ConnectionSettings
        where T2 : EditConnectionSettingsViewModel<T1>, new()
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationMessageFactory<AddConnectionViewModel<T1, T2>> _navigationMessageFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderViewModel{T1,T2}" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="navigationMessageFactory">The navigation message factory.</param>
        /// <param name="providerName">The provider name.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public BuildProviderViewModel(IEventAggregator eventAggregator, INavigationMessageFactory<AddConnectionViewModel<T1, T2>> navigationMessageFactory, string providerName)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(navigationMessageFactory).IsNotNull();
            Ensure.That(providerName).IsNotNullOrWhiteSpace();

            _eventAggregator = eventAggregator;
            _navigationMessageFactory = navigationMessageFactory;
            ProviderName = providerName;
        }

        /// <summary>
        /// Gets the provider name.
        /// </summary>
        /// <value>
        /// The provider name.
        /// </value>
        public string ProviderName { get; }

        /// <summary>
        /// Navigates to the view to add connection.
        /// </summary>
        public void AddConnection()
        {
            var message = _navigationMessageFactory.Create();

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}