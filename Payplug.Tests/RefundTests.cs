namespace Payplug.Tests
{
    using System;
    using System.Net;
    using NUnit.Framework;
    using Payplug;
    using Payplug.Exceptions;

    [TestFixture]
    public class RefundTests
    {
        private const string PaymentID = "pay_5iHMDxy4ABR4YBVW4UscIn";
        private const string RefundID = "re_3NxGqPfSGMHQgLSZH0Mv3B";

        [Test]
        public void RefundThrowOnEmptySecretKey()
        {
            Configuration.SecretKey = string.Empty;

            Assert.Throws<ConfigurationException>(delegate { Refund.Create(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Refund.CreateRaw(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Refund.List(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Refund.ListRaw(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Refund.Retrieve(PaymentID, RefundID); });
            Assert.Throws<ConfigurationException>(delegate { Refund.RetrieveRaw(PaymentID, RefundID); });
        }

#if !__MonoCS__
        [Test]
        public void RefundThrowOnMissingSecurityProtocol()
        {
            Configuration.SecretKey = "mysecretkey";
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11;
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls12;

            Assert.Throws<ConfigurationException>(delegate { Refund.Create(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Refund.CreateRaw(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Refund.List(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Refund.ListRaw(PaymentID); });
            Assert.Throws<ConfigurationException>(delegate { Refund.Retrieve(PaymentID, RefundID); });
            Assert.Throws<ConfigurationException>(delegate { Refund.RetrieveRaw(PaymentID, RefundID); });
        }
#endif

        [Test]
        public void RefundThrowOnNullArgs()
        {
            Assert.Throws<ArgumentNullException>(delegate { Refund.Create(null); });
            Assert.Throws<ArgumentNullException>(delegate { Refund.CreateRaw(null); });
            Assert.Throws<ArgumentNullException>(delegate { Refund.List(null); });
            Assert.Throws<ArgumentNullException>(delegate { Refund.ListRaw(null); });
            Assert.Throws<ArgumentNullException>(delegate { Refund.Retrieve(null, null); });
            Assert.Throws<ArgumentNullException>(delegate { Refund.RetrieveRaw(null, null); });
        }
    }
}