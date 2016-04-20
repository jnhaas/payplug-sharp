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
        private const string PaymentID = "pay_5iHMDxy4ABR4YBVW4UscIn";
        private const string PaymentRaw = @"{""id"": ""pay_5iHMDxy4ABR4YBVW4UscIn""}";
        private Dictionary<string, dynamic> paymentData;

        [SetUp]
        public void Init()
        {
            this.paymentData = new Dictionary<string, dynamic>
            {
                { "id", PaymentID }
            };
        }

        [Test]
        public void PaymentThrowOnEmptySecretKey()
        {
            Configuration.SecretKey = string.Empty;

            Assert.Throws<ConfigurationException>(delegate { Payment.Create(this.paymentData); });
            Assert.Throws<ConfigurationException>(delegate { Payment.CreateRaw(PaymentRaw); });
            Assert.Throws<ConfigurationException>(delegate { Payment.List(); });
            Assert.Throws<ConfigurationException>(delegate { Payment.ListRaw(); });
            Assert.Throws<ConfigurationException>(delegate { Payment.Retrieve(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Payment.RetrieveRaw(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Payment.Abort(PaymentID); });
        }

        [Test]
        public void PaymentThrowOnMissingSecurityProtocol()
        {
            Configuration.SecretKey = "mysecretkey";
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls;
#if !__MonoCS__
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11;
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls12;
#endif
            Assert.Throws<ConfigurationException>(delegate { Payment.Create(this.paymentData); });
            Assert.Throws<ConfigurationException>(delegate { Payment.CreateRaw(PaymentRaw); });
            Assert.Throws<ConfigurationException>(delegate { Payment.List(); });
            Assert.Throws<ConfigurationException>(delegate { Payment.ListRaw(); });
            Assert.Throws<ConfigurationException>(delegate { Payment.Retrieve(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Payment.RetrieveRaw(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Payment.Abort(PaymentID); });
        }

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
