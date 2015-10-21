namespace Payplug.Exceptions
{
    /// <summary>
    /// Exception thrown when the notification you got is invalid.
    /// </summary>
    [System.Serializable]
    public class InvalidAPIResourceException : System.ArgumentException
    {
        public InvalidAPIResourceException()
        {
        }

        public InvalidAPIResourceException(string message) : base(message)
        {
        }

        public InvalidAPIResourceException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected InvalidAPIResourceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
