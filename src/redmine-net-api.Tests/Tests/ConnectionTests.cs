using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using redmine.net.api.Tests;
using redmine.net.api.Tests.Infrastructure;
using RedmineClient.Exceptions;
using RedmineClient.Types;
using Xunit;

namespace RedmineClient.Tests.Tests
{
    [Trait("Redmine-api", "Credentials")]
#if (!NET40)
    [Collection("RedmineCollection")]
#endif
    [Order(1)]
    public class ConnectionTests
    {
        private readonly RedmineFixture fixture;

#if (NET40)
        public ConnectionTests()
        {
            fixture = new RedmineFixture();
            fixture.ClientSettings.HostUrl = "http://192.168.1.52:8089";
        }
#endif
        public ConnectionTests(RedmineFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Should_Throw_Redmine_Exception_When_No_Authentication_Set()
        {
            Assert.Throws<UnauthorizedException>(() => fixture.RedmineApiClient.GetCurrentUser());
        }

        [Fact]
        public void Should_Connect_With_Username_And_Password()
        {
            fixture.ClientSettings.HostUrl = "http://192.168.1.52:8089";
            fixture.ClientSettings.Credentials = new NetworkCredential("zapadi","1qaz2wsx");
            
            var expectedUser = fixture.RedmineApiClient.GetCurrentUser();
            Assert.NotNull(expectedUser);
            Assert.Equal(expectedUser.Login, Helper.Username);
        }

        [Fact]
        public void Should_Connect_With_Api_Key()
        {
            fixture.ClientSettings.HostUrl = "http://192.168.1.52:8089";
            fixture.ClientSettings.UseApiKey = true;
            fixture.ClientSettings.ApiKey =("a96e35d02bc6a6dbe655b83a2f6db57b82df2dff");

            var expectedUser = fixture.RedmineApiClient.GetCurrentUser(User.Include.Groups, User.Include.Memberships, User.Include.CustomFields);
            Assert.NotNull(expectedUser);
            Assert.Equal(expectedUser.ApiKey, fixture.ClientSettings.ApiKey);
        }
    }
}
