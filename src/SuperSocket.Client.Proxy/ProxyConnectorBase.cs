using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SuperSocket.Channel;
using SuperSocket.Client;
using SuperSocket.ProtoBase;

namespace SuperSocket.Client.Proxy
{

    public abstract class ProxyConnectorBase : ConnectorBase
    {
        private EndPoint _proxyEndPoint;
        /// <summary>
        /// ��ʼ������������
        /// </summary>
        /// <param name="proxyEndPoint">���������ʶ</param>
        public ProxyConnectorBase(EndPoint proxyEndPoint)
        {
            _proxyEndPoint = proxyEndPoint;
        }
        /// <summary>
        /// �첽���Ӵ���
        /// </summary>
        /// <param name="remoteEndPoint">Զ�������ʶ</param>
        /// <param name="state">����״̬</param>
        /// <param name="cancellationToken">�첽������ʶToken</param>
        /// <returns></returns>
        protected abstract ValueTask<ConnectState> ConnectProxyAsync(EndPoint remoteEndPoint, ConnectState state, CancellationToken cancellationToken);
        /// <summary>
        /// �첽����
        /// </summary>
        /// <param name="remoteEndPoint">Զ�������ʶ</param>
        /// <param name="state">����״̬</param>
        /// <param name="cancellationToken">�첽������ʾ</param>
        /// <returns></returns>
        protected override async ValueTask<ConnectState> ConnectAsync(EndPoint remoteEndPoint, ConnectState state, CancellationToken cancellationToken)
        {
            var socketConnector = new SocketConnector() as IConnector;
            var proxyEndPoint = _proxyEndPoint;

            ConnectState result;
            
            try
            {
                result = await socketConnector.ConnectAsync(proxyEndPoint, null, cancellationToken);
                
                if (!result.Result)
                    return result;
            }
            catch (Exception e)
            {
                return new ConnectState
                {
                    Result = false,
                    Exception = e
                };
            }

            return await ConnectProxyAsync(remoteEndPoint, state, cancellationToken);
        }
    }
}