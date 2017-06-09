﻿// <copyright file="EditConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.ViewModels
{
    using EnsureThat;
    using Settings;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="EditConnectionViewModel" /> class.
    /// </summary>
    public class EditConnectionViewModel : WPF.ViewModels.EditConnectionViewModel<CircleCI.ConnectionSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel" /> class.
        /// </summary>
        /// <param name="settingsRepository">The build provider settings repository.</param>
        /// <param name="connectionSettingsFactory">The connection settings factory.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        /// <param name="projectsToMonitorFactory">The projects to monitor factory.</param>
        /// <param name="currentSettings">The current settings.</param>
        public EditConnectionViewModel(
            IConnectionSettingsRepository settingsRepository,
            IConnectionSettingsViewModelFactory<CircleCI.ConnectionSettings, ConnectionSettingsViewModel> connectionSettingsFactory,
            IBuildProviderStrategy buildProviderStrategy,
            IProjectToMonitorViewModelFactory projectToMonitorFactory,
            IProjectsToMonitorViewModelFactory projectsToMonitorFactory,
            CircleCI.ConnectionSettings currentSettings)
            : base(
                  settingsRepository,
                  buildProviderStrategy,
                  projectToMonitorFactory,
                  projectsToMonitorFactory,
                  currentSettings)
        {
            Ensure.That(connectionSettingsFactory).IsNotNull();

            var settings = connectionSettingsFactory.Create();

            settings.Name = currentSettings.Name;
            settings.Token = currentSettings.Token;
            settings.IsDirty = false;

            Settings = settings;

            TryConnect();
        }
    }
}