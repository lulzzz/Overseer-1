﻿// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildProvider : BuildProvider<ConnectionSettings>
    {
        private readonly Api.IApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvider" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="apiClient">The API client.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public BuildProvider(ConnectionSettings settings, Api.IApiClient apiClient)
            : base(settings)
        {
            Ensure.That(apiClient).IsNotNull();

            _apiClient = apiClient;
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<IProject>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            var projects = await _apiClient.GetProjectsAsync(cancellationToken).ConfigureAwait(false);

            return projects.Select(project => new Project(project)).ToArray();
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId, CancellationToken cancellationToken)
        {
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            var projects = await _apiClient.GetProjectsAsync(cancellationToken).ConfigureAwait(false);

            var project = projects.FirstOrDefault(p => p.Name == projectId);

            if (project == null)
            {
                return new IBuild[] { };
            }

            var builds = await _apiClient.GetBuildsAsync(project.VcsType, project.Username, project.Name, 0, Settings.BuildsPerProject, cancellationToken).ConfigureAwait(false);

            return builds.Select(build => new Build(build)).ToArray();
        }
    }
}