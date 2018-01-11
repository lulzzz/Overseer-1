﻿// <copyright file="WizardFinishViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using Caliburn.Micro;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="WizardFinishViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class WizardFinishViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WizardFinishViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public WizardFinishViewModel(IPlatformProvider platformProvider)
            : base(platformProvider)
        {
        }

        public void Finish()
        {
            (Parent as IClose)?.TryClose(true);
        }
    }
}
