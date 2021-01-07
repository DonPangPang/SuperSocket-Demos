using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SuperSocket.Channel;

namespace SuperSocket
{
    public interface IPackageHandlingScheduler<TPackageInfo>
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="packageHandler">������</param>
        /// <param name="errorHandler">������</param>
        void Initialize(IPackageHandler<TPackageInfo> packageHandler, Func<IAppSession, PackageHandlingException<TPackageInfo>, ValueTask<bool>> errorHandler);
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="package">Package</param>
        /// <returns></returns>
        ValueTask HandlePackage(IAppSession session, TPackageInfo package);
    }
}