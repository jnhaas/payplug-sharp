﻿namespace Payplug.Exceptions
{
    /// <summary>
    /// Exception thrown when the library has not been set up correctly.
    /// </summary>
    [System.Serializable]
    public class ConfigurationException : System.Exception
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string message) : base(message)
        {
        }

        public ConfigurationException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected ConfigurationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}