using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SuperSocket.Client;

namespace SuperSocket.Client.Proxy
{
    public class Socks4Connector : ConnectorBase
    {
        /// <summary>
        /// �첽����
        /// </summary>
        /// <param name="remoteEndPoint">Զ�������ʶ</param>
        /// <param name="state">����״̬</param>
        /// <param name="cancellationToken">�첽������ʶToken</param>
        /// <returns></returns>
        protected override ValueTask<ConnectState> ConnectAsync(EndPoint remoteEndPoint, ConnectState state, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}