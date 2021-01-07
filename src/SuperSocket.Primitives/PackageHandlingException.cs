using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSocket.Channel;

namespace SuperSocket
{
    public class PackageHandlingException<TPackageInfo> : Exception
    {
        public TPackageInfo Package { get; private set; }
        /// <summary>
        /// �������쳣
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="package">��</param>
        /// <param name="e">�쳣</param>
        public PackageHandlingException(string message, TPackageInfo package, Exception e)
            : base(message, e)
        {
            Package = package;
        }
    }
}