﻿// <copyright file="EditConnectionSettingsViewModelValidator.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Validators
{
    using System;
    using FluentValidation;
    using ViewModels;

    /// <summary>
    /// The <see cref="EditConnectionSettingsViewModelValidator" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditConnectionSettingsViewModelValidator : WPF.Validators.EditConnectionSettingsViewModelValidator<EditConnectionSettingsViewModel, ConnectionSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditConnectionSettingsViewModelValidator" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public EditConnectionSettingsViewModelValidator()
        {
            RuleFor(viewModel => viewModel.Url)
                .NotEmpty()
                .Must(url =>
                {
                    Uri result;

                    return Uri.TryCreate(url, UriKind.Absolute, out result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
                })
                .WithMessage(Properties.Resources.EditConnectionSettings_Validation_Url);

            RuleFor(viewModel => viewModel.Token)
                .NotEmpty()
                .WithMessage(Properties.Resources.EditConnectionSettings_Validation_Token);
        }
    }
}
