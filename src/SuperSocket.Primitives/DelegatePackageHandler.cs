using System;
using System.Threading.Tasks;
using SuperSocket.Channel;

namespace SuperSocket
{
    public class DelegatePackageHandler<TReceivePackageInfo> : IPackageHandler<TReceivePackageInfo>
    {

        Func<IAppSession, TReceivePackageInfo, ValueTask> _func;
        /// <summary>
        /// ������ί��
        /// </summary>
        /// <param name="func"></param>
        public DelegatePackageHandler(Func<IAppSession, TReceivePackageInfo, ValueTask> func)
        {
            _func = func;
        }
        /// <summary>
        /// �첽����
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="package">���ܰ���Ϣ</param>
        /// <returns></returns>
        public async ValueTask Handle(IAppSession session, TReceivePackageInfo package)
        {
            await _func(session, package);
        }
    }
}