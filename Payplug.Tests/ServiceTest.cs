namespace Payplug.Tests
{
    using System;
    using System.Net;
    using NUnit.Framework;
    using Payplug.Core;
    using Payplug.Exceptions;

    [TestFixture]
    public class ServiceTest
    {
        [SetUp]
        public void Init()
        {
            Configuration.SecretKey = "mykey";
        }

        [Test]
        public void ServiceThrowOnMissingSecurityProtocol()
        {
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls;
#if !__MonoCS__
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11;
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls12;
#endif
            Assert.Throws<ConfigurationException>(delegate { Service.Get(new Uri("https://www.ssllabs.com:10303/")); });
            Assert.Throws<ConfigurationException>(delegate { Service.Patch(new Uri("https://www.ssllabs.com:10303/"), null); });
            Assert.Throws<ConfigurationException>(delegate { Service.Post(new Uri("https://www.ssllabs.com:10303/"), null); });
        }

        [Test]
        public void ServiceWorkOnTls()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls;
            Assert.IsNotEmpty(Service.Get(new Uri("https://www.ssllabs.com:10301/")));
        }

#if !__MonoCS__
        [Test]
        public void ServiceWorkOnTls11()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11;
            Assert.IsNotEmpty(Service.Get(new Uri("https://www.ssllabs.com:10302/")));
        }

        [Test]
        public void ServiceWorkOnTls12()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            Assert.IsNotEmpty(Service.Get(new Uri("https://www.ssllabs.com:10303/")));
        }
#endif

        [Test]
        public void ServiceThrowOnWebException5xx()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls;
            Assert.Throws<WebException>(delegate { Service.Get(new Uri("http://httpstat.us/500")); });
            Assert.Throws<WebException>(delegate { Service.Get(new Uri("http://httpstat.us/504")); });
        }

        [Test]
        public void ServiceThrowOnClientWebException4xx()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls;
            Assert.Throws<ClientWebException>(delegate { Service.Get(new Uri("http://httpstat.us/400")); });
            Assert.Throws<ClientWebException>(delegate { Service.Get(new Uri("http://httpstat.us/404")); });
        }

        [Test]
        public void ServiceThrowOnWebException3xx()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls;
            Assert.Throws<WebException>(delegate { Service.Get(new Uri("http://httpstat.us/300")); });
        }

        [Test]
        public void ServiceReleaseServerCertificateValidationCallback()
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls;
            Service.Get(new Uri("http://httpstat.us/200"));
            Assert.IsNull(ServicePointManager.ServerCertificateValidationCallback);
        }

        [Test]
        public void ServiceThrowOnBadCertificate()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls;
            Assert.Throws<WebException>(delegate { Service.Get(new Uri("https://mismatched.stripe.com/")); });
        }

        [Test]
        public void ServiceWorksOnUtfChar()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls;
            Assert.DoesNotThrow(delegate { Service.Post(new Uri("http://jsonplaceholder.typicode.com.com/posts"), "très spécial"); });
        }
    }
}
