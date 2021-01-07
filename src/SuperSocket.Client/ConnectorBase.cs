using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SuperSocket.Client
{
    public abstract class ConnectorBase : IConnector
    {
        public IConnector NextConnector { get; private set; }

        public ConnectorBase()
        {

        }
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="nextConnector">��һ��������</param>
        public ConnectorBase(IConnector nextConnector)
            : this()
        {
            NextConnector = nextConnector;
        }
        /// <summary>
        /// ���󷽷����������ӣ��첽��
        /// </summary>
        /// <param name="remoteEndPoint">Զ�������ʶ</param>
        /// <param name="state">����״̬</param>
        /// <param name="cancellationToken">����ȡ���첽����</param>
        /// <returns></returns>
        protected abstract ValueTask<ConnectState> ConnectAsync(EndPoint remoteEndPoint, ConnectState state, CancellationToken cancellationToken);
        /// <summary>
        /// �������ӣ��첽��
        /// </summary>
        /// <param name="remoteEndPoint">Զ�̱�ʶ�����ַ</param>
        /// <param name="state">����״̬</param>
        /// <param name="cancellationToken">�첽������Token������ȡ���첽����</param>
        /// <returns>����״̬</returns>
        async ValueTask<ConnectState> IConnector.ConnectAsync(EndPoint remoteEndPoint, ConnectState state, CancellationToken cancellationToken)
        {
            var result = await ConnectAsync(remoteEndPoint, state, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                return ConnectState.CancelledState;

            var nextConnector = NextConnector;

            if (!result.Result || nextConnector == null)
                return result;            

            return await nextConnector.ConnectAsync(remoteEndPoint, result, cancellationToken);
        }
    }
}