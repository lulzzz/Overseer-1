﻿// <copyright file="IViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="IViewModel" /> interface.
    /// </summary>
    public interface IViewModel : IViewAware, IClose, IHaveDisplayName, IChild
    {
    }
}