using System;

namespace SuperSocket.ProtoBase
{
    public class ProtocolException : Exception
    {
        /// <summary>
        /// Э���쳣
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exception">�쳣��Ϣ</param>
        public ProtocolException(string message, Exception exception)
            : base(message, exception)
        {

        }
        /// <summary>
        /// Э���쳣
        /// </summary>
        /// <param name="message">�쳣��Ϣ</param>
        public ProtocolException(string message)
            : base(message)
        {

        }
    }
}