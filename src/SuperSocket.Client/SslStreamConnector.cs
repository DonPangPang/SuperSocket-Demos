using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace SuperSocket.Client
{
    public class SslStreamConnector : ConnectorBase
    {
        /// <summary>
        /// SSL��֤ѡ��
        /// </summary>
        public SslClientAuthenticationOptions Options { get; private set; }
        /// <summary>
        /// ��ʼ��SSL������
        /// </summary>
        /// <param name="options">SSL��֤ѡ��</param>
        public SslStreamConnector(SslClientAuthenticationOptions options)
            : base()
        {
            Options = options;
        }
        /// <summary>
        /// ��ʼ��SSL������
        /// </summary>
        /// <param name="options">SSL��֤ѡ��</param>
        /// <param name="nextConnector">������</param>
        public SslStreamConnector(SslClientAuthenticationOptions options, IConnector nextConnector)
            : base(nextConnector)
        {
            Options = options;
        }
        /// <summary>
        /// ����SSL���ӣ��첽��
        /// </summary>
        /// <param name="remoteEndPoint">Զ�������ʶ</param>
        /// <param name="state">����״̬</param>
        /// <param name="cancellationToken">�첽����Token</param>
        /// <returns></returns>
        protected override async ValueTask<ConnectState> ConnectAsync(EndPoint remoteEndPoint, ConnectState state, CancellationToken cancellationToken)
        {
            var targetHost = Options.TargetHost;

            if (string.IsNullOrEmpty(targetHost))
            {
                if (remoteEndPoint is DnsEndPoint remoteDnsEndPoint)
                    targetHost = remoteDnsEndPoint.Host;
                else if (remoteEndPoint is IPEndPoint remoteIPEndPoint)
                    targetHost = remoteIPEndPoint.Address.ToString();

                Options.TargetHost = targetHost;
            }

            var socket = state.Socket;

            if (socket == null)
                throw new Exception("Socket from previous connector is null.");
            
            try
            {
                var stream = new SslStream(new NetworkStream(socket, true), false);
                await stream.AuthenticateAsClientAsync(Options, cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                    return ConnectState.CancelledState;

                state.Stream = stream;
                return state;
            }
            catch (Exception e)
            {
                return new ConnectState
                {
                    Result = false,
                    Exception = e
                };
            }
        }
    }
}