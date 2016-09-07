﻿// <copyright file="BuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="BuildViewModel" /> class.
    /// </summary>
    public class BuildViewModel : PropertyChangedBase
    {
        public BuildViewModel()
        {
            CommitMessage = "Bug #238 Fixed a bug.";
            CommitUser = "john";
            Branch = "master";
            Revision = "11982efa";
            Version = "12.324.4410.1134";
            Started = DateTime.Now;
            Finished = DateTime.Now;
        }

        /// <summary>
        /// Gets the commit message.
        /// </summary>
        /// <value>
        /// The commit message.
        /// </value>
        public string CommitMessage { get; }

        /// <summary>
        /// Gets the commit user.
        /// </summary>
        /// <value>
        /// The commit user.
        /// </value>
        public string CommitUser { get; }

        /// <summary>
        /// Gets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        /// <value>
        /// The revision.
        /// </value>
        public string Revision { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; }

        /// <summary>
        /// Gets the started date.
        /// </summary>
        /// <value>
        /// The started date.
        /// </value>
        public DateTime? Started { get; }

        /// <summary>
        /// Gets finished date.
        /// </summary>
        /// <value>
        /// The finished date.
        /// </value>
        public DateTime? Finished { get; }
    }
}