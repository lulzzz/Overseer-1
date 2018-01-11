﻿// <copyright file="EditConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Overseer.Logging;
    using Settings;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="EditConnectionViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditConnectionViewModel : WPF.ViewModels.EditConnectionViewModel<AppVeyor.ConnectionSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="logService">The log service.</param>
        /// <param name="connectionSettingsRepository">The build provider settings repository.</param>
        /// <param name="editConnectionSettingsFactory">The connection settings factory.</param>
        /// <param name="buildProviderStrategy">The build provider strategy.</param>
        /// <param name="editTrackedProjectViewModelFactory">The tracked project factory.</param>
        /// <param name="editTrackedProjectsViewModelFactory">The tracked projects factory.</param>
        /// <param name="currentSettings">The current settings.</param>
#pragma warning disable S107 // Methods should not have too many parameters

        // ReSharper disable once InheritdocConsiderUsage
        public EditConnectionViewModel(
            IPlatformProvider platformProvider,
            ILogService logService,
            IConnectionSettingsRepository connectionSettingsRepository,
            IEditConnectionSettingsViewModelFactory<AppVeyor.ConnectionSettings, EditConnectionSettingsViewModel> editConnectionSettingsFactory,
            IBuildProviderStrategy buildProviderStrategy,
            IEditTrackedProjectViewModelFactory editTrackedProjectViewModelFactory,
            IEditTrackedProjectsViewModelFactory editTrackedProjectsViewModelFactory,
            AppVeyor.ConnectionSettings currentSettings)
            : base(
                  platformProvider,
                  logService,
                  connectionSettingsRepository,
                  buildProviderStrategy,
                  editTrackedProjectViewModelFactory,
                  editTrackedProjectsViewModelFactory,
                  currentSettings)
        {
#pragma warning restore S107 // Methods should not have too many parameters
            Ensure.That(editConnectionSettingsFactory).IsNotNull();

            var settings = editConnectionSettingsFactory.Create();

            settings.Name = currentSettings.Name;
            settings.Token = currentSettings.Token;
            settings.BuildsPerProject = currentSettings.BuildsPerProject;
            settings.IsDirty = false;

            Settings = settings;

            TryConnect();
        }
    }
}
