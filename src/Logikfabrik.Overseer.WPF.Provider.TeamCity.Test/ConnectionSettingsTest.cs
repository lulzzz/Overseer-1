﻿// <copyright file="ConnectionSettingsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Test
{
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class ConnectionSettingsTest
    {
        [Fact]
        public void CanGetProviderType()
        {
            var settings = new ConnectionSettings();

            settings.ProviderType.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void CanGetUrl(string url)
        {
            var settings = new ConnectionSettings { Url = url };

            settings.Url.ShouldBe(url);
        }

        public void CanGetAuthenticationType()
        {
            // TODO: This unit test.
        }

        [Theory]
        [AutoData]
        public void CanGetVersion(string version)
        {
            var settings = new ConnectionSettings { Version = version };

            settings.Version.ShouldBe(version);
        }

        [Theory]
        [AutoData]
        public void CanGetUsername(string username)
        {
            var settings = new ConnectionSettings { Username = username };

            settings.Username.ShouldBe(username);
        }

        [Theory]
        [AutoData]
        public void CanGetPassword(string password)
        {
            var settings = new ConnectionSettings { Password = password };

            settings.Password.ShouldBe(password);
        }
    }
}
