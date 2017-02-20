﻿// <copyright file="Connection.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="Connection" /> class.
    /// </summary>
    public class Connection : IConnection
    {
        private readonly IBuildProviderFactory _buildProviderFactory;
        private IBuildProvider _provider;
        private ConnectionSettings _settings;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="buildProviderFactory">The build provider factory.</param>
        /// <param name="settings">The settings.</param>
        public Connection(IBuildProviderFactory buildProviderFactory, ConnectionSettings settings)
        {
            Ensure.That(buildProviderFactory).IsNotNull();
            Ensure.That(settings).IsNotNull();

            _buildProviderFactory = buildProviderFactory;
            _settings = settings;
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public ConnectionSettings Settings
        {
            get
            {
                return _settings.Clone();
            }

            set
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException(GetType().FullName);
                }

                Ensure.That(value).IsNotNull();
                Ensure.That(() => value.Id == _settings.Id, nameof(value)).IsTrue();

                _settings = value.Clone();
            }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public async Task<IEnumerable<IProject>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            return await GetProvider().GetProjectsAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the builds for the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public async Task<IEnumerable<IBuild>> GetBuildsAsync(IProject project, CancellationToken cancellationToken)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            Ensure.That(project).IsNotNull();

            return await GetProvider().GetBuildsAsync(project.Id, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            // ReSharper disable once InvertIf
            if (disposing)
            {
                _provider?.Dispose();
            }

            _isDisposed = true;
        }

        private IBuildProvider GetProvider()
        {
            if (_provider == null)
            {
                _provider = _buildProviderFactory.Create(_settings);
            }
            else
            {
                if (_provider.Settings.Equals(_settings))
                {
                    return _provider;
                }

                _provider.Dispose();
                _provider = _buildProviderFactory.Create(_settings);
            }

            return _provider;
        }
    }
}