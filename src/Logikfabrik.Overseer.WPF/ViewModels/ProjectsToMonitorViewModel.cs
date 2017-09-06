﻿// <copyright file="ProjectsToMonitorViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;
    using Caliburn.Micro;
    using EnsureThat;
    using Text;

    /// <summary>
    /// The <see cref="ProjectsToMonitorViewModel" /> class.
    /// </summary>
    public class ProjectsToMonitorViewModel : PropertyChangedBase
    {
        private readonly CollectionViewSource _filteredProjects;
        private string _filter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsToMonitorViewModel" /> class.
        /// </summary>
        /// <param name="projects">The projects.</param>
        public ProjectsToMonitorViewModel(IEnumerable<ProjectToMonitorViewModel> projects)
        {
            Ensure.That(projects).IsNotNull();

            Projects = new ObservableCollection<ProjectToMonitorViewModel>(projects);

            _filteredProjects = new CollectionViewSource
            {
                Source = Projects
            };

            _filteredProjects.Filter += (sender, e) =>
            {
                var project = (ProjectToMonitorViewModel)e.Item;

                e.Accepted = string.IsNullOrWhiteSpace(_filter) || DamerauLevenshtein.GetDistance(project.Name?.ToLowerInvariant(), _filter.ToLowerInvariant()) < 5;
            };
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public IEnumerable<ProjectToMonitorViewModel> Projects { get; }

        /// <summary>
        /// Gets the filtered projects.
        /// </summary>
        /// <value>
        /// The filtered projects.
        /// </value>
        public ICollectionView FilteredProjects => _filteredProjects.View;

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        public string Filter
        {
            get
            {
                return _filter;
            }

            set
            {
                _filter = value;
                NotifyOfPropertyChange(() => Filter);

                _filteredProjects.View.Refresh();
            }
        }

        /// <summary>
        /// Monitor all projects.
        /// </summary>
        public void MonitorAll()
        {
            ToggleMonitoring(true);
        }

        /// <summary>
        /// Monitor no projects.
        /// </summary>
        public void MonitorNone()
        {
            ToggleMonitoring(false);
        }

        private void ToggleMonitoring(bool monitor)
        {
            var projects = Projects.Where(project => project.Monitor != monitor).ToArray();

            foreach (var project in projects)
            {
                project.Monitor = monitor;
            }
        }
    }
}
