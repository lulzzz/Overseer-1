﻿// <copyright file="BuildState.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    /// <summary>
    /// The <see cref="BuildState" /> enumeration.
    /// </summary>
    public enum BuildState
    {
        Created,

        Booting,

        Started,

        Passed,

        Failed,

        Errored
    }
}
