﻿// <copyright file="StartWizardViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Logikfabrik.Overseer.Settings;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="StartWizardViewModel" /> class.
    /// </summary>
#pragma warning disable S110 // Inheritance tree of classes should not be too deep

    // ReSharper disable once InheritdocConsiderUsage
    public sealed class StartWizardViewModel : Conductor<IViewModel>
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        private readonly IConnectionSettingsEncrypter _encrypter;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartWizardViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="encrypter"></param>
        /// <param name="welcomeWizardStepViewModel">The welcome wizard step view model.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public StartWizardViewModel(IEventAggregator eventAggregator, IConnectionSettingsEncrypter encrypter, StartWizardStepViewModel welcomeWizardStepViewModel)
            : base(eventAggregator)
        {
            Ensure.That(encrypter).IsNotNull();
            Ensure.That(welcomeWizardStepViewModel).IsNotNull();

            _encrypter = encrypter;

            DisplayName = "Welcome";

            ActivateItem(welcomeWizardStepViewModel);
        }

        public override void CanClose(Action<bool> callback)
        {
            callback(_encrypter.HasPassPhrase);
        }
    }
}