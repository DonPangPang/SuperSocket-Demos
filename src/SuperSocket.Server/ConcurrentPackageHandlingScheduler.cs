using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SuperSocket.Channel;

namespace SuperSocket.Server
{
    public class ConcurrentPackageHandlingScheduler<TPackageInfo> : PackageHandlingSchedulerBase<TPackageInfo>
    {
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="package">���ݰ�</param>
        /// <returns></returns>
        public override ValueTask HandlePackage(IAppSession session, TPackageInfo package)
        {
            HandlePackageInternal(session, package).DoNotAwait();
            return new ValueTask();
        }
    }
}