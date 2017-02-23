﻿// <copyright file="IProjectViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;

    /// <summary>
    /// The <see cref="IProjectViewModelFactory" /> interface.
    /// </summary>
    public interface IProjectViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="projectName">The project name.</param>
        /// <returns>A view model.</returns>
        ProjectViewModel Create(Guid settingsId, string projectId, string projectName);
    }
}
