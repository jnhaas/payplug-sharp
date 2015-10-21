namespace Payplug.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Payplug;
    using Payplug.Core;

    [TestFixture]
    public class RoutesTest
    {
        [Test]
        public void RoutesGenerateValidURIWithoutFormat()
        {
            Assert.AreEqual(Routes.Uri(null), new Uri("https://api.payplug.com/v1"));
            Assert.AreEqual(Routes.Uri("/path"), new Uri("https://api.payplug.com/v1/path"));
            Assert.AreEqual(Routes.Uri("path"), new Uri("https://api.payplug.com/v1/path"));
            Assert.AreEqual(Routes.Uri("/path/{ID}/route"), new Uri("https://api.payplug.com/v1/path/{ID}/route"));
        }

        [Test]
        public void RoutesGenerateValidURIWithFormat()
        {
            var paramsDict = new Dictionary<string, string>
            {
                { "ID", "1234" },
                { "KEY", "value" }
            };
            Assert.AreEqual(Routes.Uri("/path/{ID}/route", paramsDict), new Uri("https://api.payplug.com/v1/path/1234/route"));
            Assert.AreEqual(Routes.Uri("/path/{ID}/route/{ID}", paramsDict), new Uri("https://api.payplug.com/v1/path/1234/route/1234"));
            Assert.AreEqual(Routes.Uri("/path/{ID}/{KEY}", paramsDict), new Uri("https://api.payplug.com/v1/path/1234/value"));
        }

        [Test]
        public void RoutesUseConfigurationApiBaseUrl()
        {
            Assert.IsTrue(Routes.Uri(null).AbsoluteUri.StartsWith(Configuration.ApiBaseUrl));
            Configuration.ApiBaseUrl = "http://apibaseurl.com";
            Assert.IsTrue(Routes.Uri(null).AbsoluteUri.StartsWith(Configuration.ApiBaseUrl));
        }

        [Test]
        public void RoutesGenerateValidQueryString()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "page", "12" },
                { "per_page", "24" },
            };
            Assert.AreEqual(Routes.Uri(null, null, queryParameters), new Uri("https://api.payplug.com/v1?page=12&per_page=24"));
        }

        [Test]
        public void RoutesGenerateEscapedQueryString()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "page&page", "12" },
            };
            Assert.AreEqual(Routes.Uri(null, null, queryParameters), new Uri("https://api.payplug.com/v1?page%26page=12"));
        }
    }
}
