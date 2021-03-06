﻿// <copyright file="ErrorLogHandlerConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using EnsureThat;
    using Overseer.Logging;

    /// <summary>
    /// The <see cref="ErrorLogHandlerConfigurator" /> class.
    /// </summary>
    public static class ErrorLogHandlerConfigurator
    {
        /// <summary>
        /// Configures error handling.
        /// </summary>
        /// <param name="currentAppDomain">The current application domain.</param>
        /// <param name="application">The application.</param>
        /// <param name="logService">The log service.</param>
        public static void Configure(AppDomain currentAppDomain, IApp application, ILogService logService)
        {
            Ensure.That(currentAppDomain).IsNotNull();
            Ensure.That(application).IsNotNull();
            Ensure.That(logService).IsNotNull();

            currentAppDomain.UnhandledException += (sender, e) =>
            {
                var exception = e.ExceptionObject as Exception;

                logService.Log(typeof(ErrorLogHandlerConfigurator), new LogEntry(LogEntryType.Error, null, exception));
            };

            application.DispatcherUnhandledException += (sender, e) =>
            {
                var exception = e.Exception;

                logService.Log(typeof(ErrorLogHandlerConfigurator), new LogEntry(LogEntryType.Error, null, exception));
            };
        }
    }
}
