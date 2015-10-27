namespace Payplug.Exceptions
{
    /// <summary>
    /// Exception thrown when the notification you got is invalid.
    /// </summary>
    [System.Serializable]
    public class InvalidApiResourceException : System.ArgumentException
    {
        public InvalidApiResourceException()
        {
        }

        public InvalidApiResourceException(string message) : base(message)
        {
        }

        public InvalidApiResourceException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected InvalidApiResourceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
