﻿// <copyright file="IConnection.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Settings;

    /// <summary>
    /// The <see cref="IConnection" /> interface.
    /// </summary>
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        ConnectionSettings Settings { get; set; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        Task<IEnumerable<IProject>> GetProjectsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the builds for the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        Task<IEnumerable<IBuild>> GetBuildsAsync(IProject project, CancellationToken cancellationToken);
    }
}