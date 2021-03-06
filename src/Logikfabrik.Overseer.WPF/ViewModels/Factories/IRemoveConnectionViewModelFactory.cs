﻿// <copyright file="IRemoveConnectionViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IRemoveConnectionViewModelFactory" /> interface.
    /// </summary>
    public interface IRemoveConnectionViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="viewConnectionViewModel">The view connection view model.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        RemoveConnectionViewModel Create(IViewConnectionViewModel viewConnectionViewModel);
    }
}