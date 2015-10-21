﻿namespace Payplug.Tests
{
    using System;
    using System.Net;
    using NUnit.Framework;
    using Payplug;
    using Payplug.Exceptions;
    using Microsoft.QualityTools.Testing.Fakes;

    [TestFixture]
    public class NotificationTest
    {
        [Test]
        public void NotificationThrowOnMalformatedJSON()
        {
            var malformatedJson = "{";
            var message = "Request body is malformed JSON.";
            Assert.Throws<InvalidAPIResourceException>(delegate { Notification.Treat(malformatedJson); }, message);
            Assert.Throws<InvalidAPIResourceException>(delegate { Notification.TreatRaw(malformatedJson); }, message);
        }

        [Test]
        public void NotificationThrowOnInvalidResource()
        {
            var missingIdJson = "{}";
            var message = "The API resource provided is invalid.";
            Assert.Throws<InvalidAPIResourceException>(delegate { Notification.Treat(missingIdJson); }, message);
            Assert.Throws<InvalidAPIResourceException>(delegate { Notification.TreatRaw(missingIdJson); }, message);
        }

#if !__MonoCS__
        [Test]
        public void NotificationThrowOnMissingSecurityProtocol()
        {
            Configuration.SecretKey = "mysecretkey";
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11;
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls12;

            var validJson = @"{""id"": ""paymentID""}";
            Assert.Throws<ConfigurationException>(delegate { Notification.Treat(validJson); });
            Assert.Throws<ConfigurationException>(delegate { Notification.TreatRaw(validJson); });
        }
#endif

        [Test]
        public void NotificationThrowOnMissingSecretKey()
        {
            Configuration.SecretKey = string.Empty;

            var validJson = @"{""id"": ""paymentID""}";
            Assert.Throws<ConfigurationException>(delegate { Notification.Treat(validJson); });
            Assert.Throws<ConfigurationException>(delegate { Notification.TreatRaw(validJson); });
        }

        [Test]
        public void NotificationThrowOnNullArgs()
        {
            Assert.Throws<ArgumentNullException>(delegate { Notification.Treat(null); });
            Assert.Throws<ArgumentNullException>(delegate { Notification.TreatRaw(null); });
        }


        [Test]
        public void NotificationThrowOnNotFound()
        {
            var validJson = @"{""id"": ""id""}";
            Configuration.SecretKey = "sk_test_a8c31522f14b48b907fa14f2fa45e3bd";

            using (ShimsContext.Create())
            {
                Exception e = new Payplug.Exceptions.Fakes.ShimClientWebException()
                {
                    StatusCodeGet = () => HttpStatusCode.NotFound
                };
                Payplug.Core.Fakes.ShimService.GetUri = (Uri a) => { throw e; };
                Assert.Throws<InvalidAPIResourceException>(delegate { Notification.Treat(validJson); }, "The resource you requested could not be found.");
                Assert.Throws<InvalidAPIResourceException>(delegate { Notification.TreatRaw(validJson); }, "The resource you requested could not be found.");
            }
        }
    }
}
