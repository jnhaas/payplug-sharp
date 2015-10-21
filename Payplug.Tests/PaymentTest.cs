namespace Payplug.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using NUnit.Framework;
    using Payplug;
    using Payplug.Exceptions;

    [TestFixture]
    public class PaymentTests
    {
        public const string PaymentID = "pay_5iHMDxy4ABR4YBVW4UscIn";
        public const string PaymentRaw = @"{""id"": ""pay_5iHMDxy4ABR4YBVW4UscIn""}";
        public Dictionary<string, dynamic> PaymentData;

        [SetUp]
        public void Init()
        {
            this.PaymentData = new Dictionary<string, dynamic>
            {
                { "id", PaymentID }
            };
        }

        [Test]
        public void PaymentThrowOnEmptySecretKey()
        {
            Configuration.SecretKey = string.Empty;

            Assert.Throws<ConfigurationException>(delegate { Payment.Create(this.PaymentData); });
            Assert.Throws<ConfigurationException>(delegate { Payment.CreateRaw(PaymentRaw); });
            Assert.Throws<ConfigurationException>(delegate { Payment.List(); });
            Assert.Throws<ConfigurationException>(delegate { Payment.ListRaw(); });
            Assert.Throws<ConfigurationException>(delegate { Payment.Retrieve(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Payment.RetrieveRaw(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Payment.Abort(PaymentID); });
        }

#if !__MonoCS__
        [Test]
        public void PaymentThrowOnMissingSecurityProtocol()
        {
            Configuration.SecretKey = "mysecretkey";
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11;
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls12;

            Assert.Throws<ConfigurationException>(delegate { Payment.Create(this.PaymentData); });
            Assert.Throws<ConfigurationException>(delegate { Payment.CreateRaw(PaymentRaw); });
            Assert.Throws<ConfigurationException>(delegate { Payment.List(); });
            Assert.Throws<ConfigurationException>(delegate { Payment.ListRaw(); });
            Assert.Throws<ConfigurationException>(delegate { Payment.Retrieve(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Payment.RetrieveRaw(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Payment.Abort(PaymentID); });
        }
#endif

        [Test]
        public void PaymentThrowOnNullArgs()
        {
            Assert.Throws<ArgumentNullException>(delegate { Payment.Create(null); });
            Assert.Throws<ArgumentNullException>(delegate { Payment.CreateRaw(null); });
            Assert.Throws<ArgumentNullException>(delegate { Payment.Retrieve(null); });
            Assert.Throws<ArgumentNullException>(delegate { Payment.RetrieveRaw(null); });
            Assert.Throws<ArgumentNullException>(delegate { Payment.Abort(null); });
        }
    }
}