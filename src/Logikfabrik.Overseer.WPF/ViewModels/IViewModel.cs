﻿// <copyright file="IViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using Navigation;

    /// <summary>
    /// The <see cref="IViewModel" /> interface.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public interface IViewModel : IClose, IHaveDisplayName, IChild, IKeepAlive, IContextAware, IViewAware
    {
    }
}