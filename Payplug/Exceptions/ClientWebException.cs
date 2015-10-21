namespace Payplug.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Net;
    using Newtonsoft.Json;

    /// <summary>
    /// Exception thrown when a request received a client error (4xx).
    /// </summary>
    [System.Serializable]
    public class ClientWebException : WebException
    {
        public ClientWebException()
        {
        }

        public ClientWebException(string message) : base(message)
        {
        }

        public ClientWebException(string message, System.Exception inner) : base(message, inner)
        {
        }

        public ClientWebException(WebException inner) : base(inner.Message, inner, inner.Status, inner.Response)
        {
            var webResponse = this.Response as HttpWebResponse;
            this.StatusCode = webResponse.StatusCode;

            using (var sr = new StreamReader(Response.GetResponseStream()))
            {
                try
                {
                    var response = sr.ReadToEnd();
                    this.Message = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response)["message"];
                }
                catch (Exception)
                {
                    this.Message = base.Message;
                }
               }
        }

        protected ClientWebException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
        public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            base.GetObjectData(serializationInfo, streamingContext);
        }

        /// <summary>
        /// Gets the http status code received.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message { get; }
    }
}