﻿// <copyright file="KernelConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System.Collections.Generic;
    using System.Reflection;
    using CacheManager.Core;
    using Caliburn.Micro;
    using EnsureThat;
    using Logging;
    using Ninject;
    using Ninject.Extensions.Factory;
    using Overseer.Logging;
    using Providers.Caching;
    using Providers.Settings;
    using Settings;
    using WPF.ViewModels;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="KernelConfigurator" /> class.
    /// </summary>
    public static class KernelConfigurator
    {
        /// <summary>
        /// Configures the specified kernel.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="modules">The modules.</param>
        public static void Configure(IKernel kernel, IEnumerable<Assembly> modules)
        {
            Ensure.That(kernel).IsNotNull();
            Ensure.That(modules).IsNotNull();

            // Business logic setup.
            kernel.Bind<IAppSettingsFactory>().ToFactory();
            kernel.Bind<ICacheManager<object>>().ToProvider<CacheManagerProvider>();
            kernel.Bind<ILogService>().To<LogService>();
            kernel.Bind<IConnectionSettingsSerializer>().ToProvider<ConnectionSettingsSerializerProvider>();
            kernel.Bind<IFileStore>().ToProvider<FileStoreProvider>();
            kernel.Bind<IDataProtector>().To<DataProtector>();
            kernel.Bind<IRegistryStore>().ToProvider<RegistryStoreProvider>();
            kernel.Bind<IConnectionSettingsEncrypter>().To<ConnectionSettingsEncrypter>().InSingletonScope();
            kernel.Bind<IConnectionSettingsStore>().To<ConnectionSettingsStore>();
            kernel.Bind<IConnectionSettingsRepository>().To<ConnectionSettingsRepository>().InSingletonScope();
            kernel.Bind<IBuildProviderStrategy>().To<BuildProviderStrategy>();
            kernel.Bind<IConnectionPool>().To<ConnectionPool>().InSingletonScope();
            kernel.Bind<IBuildMonitor>().To<BuildMonitor>().InSingletonScope();

            // Caliburn Micro setup.
            kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            // WPF client setup.
            kernel.Bind<IUILogService>().To<UILogService>();
            kernel.Bind<INotificationManager>().To<NotificationManager>();
            kernel.Bind<IBuildNotificationViewModelFactory>().ToFactory();
            kernel.Bind<IBuildNotificationManager>().To<BuildNotificationManager>().InSingletonScope();
            kernel.Bind<IProjectToMonitorViewModelFactory>().ToFactory();
            kernel.Bind<IProjectsToMonitorViewModelFactory>().ToFactory();
            kernel.Bind<IChangeViewModelFactory>().ToFactory();
            kernel.Bind<IBuildViewModelFactory>().ToFactory();
            kernel.Bind<IProjectViewModelFactory>().ToFactory();
            kernel.Bind<IRemoveConnectionViewModelFactory>().ToFactory();
            kernel.Bind<IConnectionViewModelStrategy>().To<ConnectionViewModelStrategy>();
            kernel.Bind<ConnectionsViewModel>().ToSelf().InSingletonScope();

            kernel.Load(modules);
        }
    }
}