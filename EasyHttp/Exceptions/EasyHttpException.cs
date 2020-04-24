using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Exceptions
{
    /// <summary>
    /// EasyHttp异常
    /// </summary>
    public class EasyHttpException : Exception
    {
        public string error;
        private Exception innerException;

        public EasyHttpException()
        {

        }

        public EasyHttpException(string message) : base(message)
        {
            this.error = message;
        }

        public EasyHttpException(string message, Exception innerException) : base(message, innerException)
        {
            this.error = message;
            this.innerException = innerException;
        }

        public string GetError()
        {
            return error;
        }
    }
}
