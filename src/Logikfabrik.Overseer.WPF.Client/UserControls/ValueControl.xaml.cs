﻿// <copyright file="ValueControl.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.UserControls
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// The <see cref="ValueControl" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
#pragma warning disable S110 // Inheritance tree of classes should not be too deep
    public partial class ValueControl : INotifyPropertyChanged
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        /// <summary>
        /// The icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Geometry), typeof(ValueControl));

        /// <summary>
        /// The value dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(ValueControl), new UIPropertyMetadata(null, PropertyChangedCallback));

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueControl" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public ValueControl()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public Geometry Icon
        {
            get
            {
                return (Geometry)GetValue(IconProperty);
            }

            set
            {
                SetValue(IconProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value
        {
            get
            {
                return GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has a value; otherwise, <c>false</c>.
        /// </value>
        public bool HasValue => Value != null;

        /// <summary>
        /// Called when a property is changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as ValueControl;

            control?.OnPropertyChanged(nameof(HasValue));
        }
    }
}