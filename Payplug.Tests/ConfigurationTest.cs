namespace Payplug.Tests
{
    using NUnit.Framework;
    using Payplug;
    using Payplug.Exceptions;

    [TestFixture]
    public class ConfigurationTest
    {
        [Test]
        public void ConfigurationThrowOnEmptySecretKey()
        {
            Configuration.SecretKey = string.Empty;
            Assert.Throws<ConfigurationException>(delegate { string a = Configuration.AuthorizationHeader; });
        }

        [Test]
        public void ConfigurationThrowOnNullSecretKey()
        {
            Configuration.SecretKey = null;
            Assert.Throws<ConfigurationException>(delegate { string a = Configuration.AuthorizationHeader; });
        }

        [Test]
        public void ConfigurationReturnValidAuthorizationHeader()
        {
            Configuration.SecretKey = "mysecretkey";
            Assert.AreEqual(Configuration.AuthorizationHeader, "Bearer mysecretkey");
        }
    }
}
