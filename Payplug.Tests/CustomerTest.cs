namespace Payplug.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using NUnit.Framework;
    using Payplug;
    using Payplug.Exceptions;

    [TestFixture]
    public class CustomerTests
    {
        private const string CustomerID = "cus_6ESfofiMiLBjC6";
        private const string CustomerRaw = @"{""id"": ""cus_6ESfofiMiLBjC6""}";
        private Dictionary<string, dynamic> customerData;

        [SetUp]
        public void Init()
        {
            this.customerData = new Dictionary<string, dynamic>
            {
                { "id", CustomerID }
            };
        }

        [Test]
        public void CustomerThrowOnEmptySecretKey()
        {
            Configuration.SecretKey = string.Empty;

            Assert.Throws<ConfigurationException>(delegate { Customer.Create(this.customerData); });
            Assert.Throws<ConfigurationException>(delegate { Customer.CreateRaw(CustomerRaw); });
            Assert.Throws<ConfigurationException>(delegate { Customer.List(); });
            Assert.Throws<ConfigurationException>(delegate { Customer.ListRaw(); });
            Assert.Throws<ConfigurationException>(delegate { Customer.Retrieve(CustomerID); });
            Assert.Throws<ConfigurationException>(delegate { Customer.RetrieveRaw(CustomerID); });
            Assert.Throws<ConfigurationException>(delegate { Customer.Update(CustomerID, this.customerData); });
            Assert.Throws<ConfigurationException>(delegate { Customer.UpdateRaw(CustomerID, CustomerRaw); });
            Assert.Throws<ConfigurationException>(delegate { Customer.Delete(CustomerID); });
        }

        [Test]
        public void CustomerThrowOnMissingSecurityProtocol()
        {
            Configuration.SecretKey = "mysecretkey";
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls;
#if !__MonoCS__
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11;
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls12;
#endif
            Assert.Throws<ConfigurationException>(delegate { Customer.Create(this.customerData); });
            Assert.Throws<ConfigurationException>(delegate { Customer.CreateRaw(CustomerRaw); });
            Assert.Throws<ConfigurationException>(delegate { Customer.List(); });
            Assert.Throws<ConfigurationException>(delegate { Customer.ListRaw(); });
            Assert.Throws<ConfigurationException>(delegate { Customer.Retrieve(CustomerID); });
            Assert.Throws<ConfigurationException>(delegate { Customer.RetrieveRaw(CustomerID); });
            Assert.Throws<ConfigurationException>(delegate { Customer.Update(CustomerID, this.customerData); });
            Assert.Throws<ConfigurationException>(delegate { Customer.UpdateRaw(CustomerID, CustomerRaw); });
            Assert.Throws<ConfigurationException>(delegate { Customer.Delete(CustomerID); });
        }

        [Test]
        public void CustomerThrowOnNullArgs()
        {
            Assert.Throws<ArgumentNullException>(delegate { Customer.Create(null); });
            Assert.Throws<ArgumentNullException>(delegate { Customer.CreateRaw(null); });
            Assert.Throws<ArgumentNullException>(delegate { Customer.Retrieve(null); });
            Assert.Throws<ArgumentNullException>(delegate { Customer.RetrieveRaw(null); });
            Assert.Throws<ArgumentNullException>(delegate { Customer.Update(null, null); });
            Assert.Throws<ArgumentNullException>(delegate { Customer.UpdateRaw(null, null); });
            Assert.Throws<ArgumentNullException>(delegate { Customer.Delete(null); });
        }
    }
}
