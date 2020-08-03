using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Infrastructure.CustomException
{
    [Serializable]
    public class AppException : ApplicationException
    {

        /// <summary>
        /// 
        /// </summary>
        public AppException()
        {
            code = 500;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public AppException(string message, int code = 500)
            : base(message)
        {
            this.code = code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        /// <param name="code"></param>
        public AppException(string message, Exception inner, int code = 500)
            : base(message, inner)
        {
            this.code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }

        protected AppException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
