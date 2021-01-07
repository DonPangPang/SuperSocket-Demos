using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SuperSocket.Client
{
    public interface IConnector
    {
/// <summary>
/// �첽����
/// </summary>
/// <param name="remoteEndPoint">Զ�������ʶ</param>
/// <param name="state">����״̬</param>
/// <param name="cancellationToken">�첽������ʶToken</param>
/// <returns></returns>
ValueTask<ConnectState> ConnectAsync(EndPoint remoteEndPoint, ConnectState state = null, CancellationToken cancellationToken = default);
/// <summary>
/// ��һ������
/// </summary>
IConnector NextConnector { get; }
    }
}