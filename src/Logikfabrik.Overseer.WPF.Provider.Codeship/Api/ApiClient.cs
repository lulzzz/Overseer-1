﻿// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;
    using Models;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public class ApiClient : IApiClient, IDisposable
    {
        private readonly string _username;
        private readonly string _password;
        private Lazy<HttpClient> _httpClient;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ApiClient(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            _username = settings.Username;
            _password = settings.Password;

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(UriUtility.BaseUri));
        }

        /// <summary>
        /// Gets the organizations.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<IEnumerable<Organization>> GetOrganizationsAsync(CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            cancellationToken.ThrowIfCancellationRequested();

            var accessToken = await GetAccessToken(_httpClient.Value, _username, _password, cancellationToken).ConfigureAwait(false);

            _httpClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", accessToken.Token);

            return accessToken.Organizations;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="perPage">Projects per page.</param>
        /// <param name="page">The current page.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<Projects> GetProjectsAsync(Guid organizationId, int perPage, int page, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(organizationId).IsNotEmpty();
            Ensure.That(perPage).IsInRange(0, 50);
            Ensure.That(page).IsInRange(1, int.MaxValue);

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"organizations/{organizationId}/projects?per_page={perPage}&page={page}";

            if (!HasAccessToken(_httpClient.Value))
            {
                await SetAccessToken(_httpClient.Value, _username, _password, cancellationToken).ConfigureAwait(false);
            }

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await SetAccessToken(_httpClient.Value, _username, _password, cancellationToken).ConfigureAwait(false);

                    // TODO: Handle infinite loops.
                    return await GetProjectsAsync(organizationId, perPage, page, cancellationToken).ConfigureAwait(false);
                }

                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<Projects>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="perPage">Projects per page.</param>
        /// <param name="page">The current page.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<Builds> GetBuildsAsync(Guid organizationId, Guid projectId, int perPage, int page, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(organizationId).IsNotEmpty();
            Ensure.That(projectId).IsNotEmpty();
            Ensure.That(perPage).IsInRange(0, 50);
            Ensure.That(page).IsInRange(1, int.MaxValue);

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"organizations/{organizationId}/projects/{projectId}/builds?per_page={perPage}&page={page}";

            if (!HasAccessToken(_httpClient.Value))
            {
                await SetAccessToken(_httpClient.Value, _username, _password, cancellationToken).ConfigureAwait(false);
            }

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await SetAccessToken(_httpClient.Value, _username, _password, cancellationToken).ConfigureAwait(false);

                    // TODO: Handle infinite loops.
                    return await GetBuildsAsync(organizationId, projectId, perPage, page, cancellationToken).ConfigureAwait(false);
                }

                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<Builds>(cancellationToken).ConfigureAwait(false);
            }
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

            if (disposing)
            {
                if (_httpClient != null)
                {
                    if (_httpClient.IsValueCreated)
                    {
                        _httpClient.Value.Dispose();
                    }

                    _httpClient = null;
                }
            }

            _isDisposed = true;
        }

        private static void SetDefaultRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static bool HasAccessToken(HttpClient client)
        {
            return client.DefaultRequestHeaders.Authorization != null;
        }

        private static async Task SetAccessToken(HttpClient client, string username, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var accessToken = await GetAccessToken(client, username, password, cancellationToken).ConfigureAwait(false);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", accessToken.Token);
        }

        private static async Task<AccessToken> GetAccessToken(HttpClient httpClient, string username, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string url = "auth";

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            httpClient.DefaultRequestHeaders.Authorization = null;

            using (var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<AccessToken>(cancellationToken).ConfigureAwait(false);
            }
        }

        private static HttpClient GetHttpClient(Uri baseUri)
        {
            var client = new HttpClient { BaseAddress = baseUri };

            SetDefaultRequestHeaders(client);

            return client;
        }
    }
}