namespace Payplug.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using NUnit.Framework;
    using Payplug;
    using Payplug.Exceptions;

    [TestFixture]
    public class CardTests
    {
        private const string CustomerID = "cus_6ESfofiMiLBjC6";
        private const string CardID = "card_6ESfofiMiLBjC6";
        private const string CardRaw = @"{""id"": ""card_6ESfofiMiLBjC6""}";
        private Dictionary<string, dynamic> cardData;

        [SetUp]
        public void Init()
        {
            this.cardData = new Dictionary<string, dynamic>
            {
                { "id", CardID }
            };
        }

        [Test]
        public void CardThrowOnEmptySecretKey()
        {
            Configuration.SecretKey = string.Empty;

            Assert.Throws<ConfigurationException>(delegate { Card.Create(CustomerID, this.cardData); });
            Assert.Throws<ConfigurationException>(delegate { Card.CreateRaw(CustomerID, CardRaw); });
            Assert.Throws<ConfigurationException>(delegate { Card.List(CustomerID); });
            Assert.Throws<ConfigurationException>(delegate { Card.ListRaw(CustomerID); });
            Assert.Throws<ConfigurationException>(delegate { Card.Retrieve(CustomerID, CardID); });
            Assert.Throws<ConfigurationException>(delegate { Card.RetrieveRaw(CustomerID, CardID); });
            Assert.Throws<ConfigurationException>(delegate { Card.Delete(CustomerID, CardID); });
        }

        [Test]
        public void CardThrowOnMissingSecurityProtocol()
        {
            Configuration.SecretKey = "mysecretkey";
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls;
#if !__MonoCS__
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11;
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls12;
#endif
            Assert.Throws<ConfigurationException>(delegate { Card.Create(CustomerID, this.cardData); });
            Assert.Throws<ConfigurationException>(delegate { Card.CreateRaw(CustomerID, CardRaw); });
            Assert.Throws<ConfigurationException>(delegate { Card.List(CustomerID); });
            Assert.Throws<ConfigurationException>(delegate { Card.ListRaw(CustomerID); });
            Assert.Throws<ConfigurationException>(delegate { Card.Retrieve(CustomerID, CardID); });
            Assert.Throws<ConfigurationException>(delegate { Card.RetrieveRaw(CustomerID, CardID); });
            Assert.Throws<ConfigurationException>(delegate { Card.Delete(CustomerID, CardID); });
        }

        [Test]
        public void CardThrowOnNullArgs()
        {
            Assert.Throws<ArgumentNullException>(delegate { Card.Create(null, null); });
            Assert.Throws<ArgumentNullException>(delegate { Card.CreateRaw(null, null); });
            Assert.Throws<ArgumentNullException>(delegate { Card.List(null); });
            Assert.Throws<ArgumentNullException>(delegate { Card.ListRaw(null); });
            Assert.Throws<ArgumentNullException>(delegate { Card.Retrieve(null, null); });
            Assert.Throws<ArgumentNullException>(delegate { Card.RetrieveRaw(null, null); });
            Assert.Throws<ArgumentNullException>(delegate { Card.Delete(null, null); });
        }
    }
}
