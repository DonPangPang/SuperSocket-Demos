using System;
using System.Buffers;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;

namespace SuperSocket.Channel
{
    public class UdpPipeChannel<TPackageInfo> : VirtualChannel<TPackageInfo>, IChannelWithSessionIdentifier
    {
        private Socket _socket;

        private IPEndPoint _remoteEndPoint;
        /// <summary>
        /// ��ʼ��Udp�ܵ�
        /// </summary>
        /// <param name="socket">Socket����</param>
        /// <param name="pipelineFilter">�ܵ�ɸѡ��</param>
        /// <param name="options">ѡ��</param>
        /// <param name="remoteEndPoint">Զ�������ʶ</param>
        public UdpPipeChannel(Socket socket, IPipelineFilter<TPackageInfo> pipelineFilter, ChannelOptions options, IPEndPoint remoteEndPoint)
            : this(socket, pipelineFilter, options, remoteEndPoint, $"{remoteEndPoint.Address}:{remoteEndPoint.Port}")
        {

        }
        /// <summary>
        /// ��ʼ��Udp�ܵ�
        /// </summary>
        /// <param name="socket">Socketͨ��</param>
        /// <param name="pipelineFilter">�ܵ�ɸѡ��</param>
        /// <param name="options">ѡ��</param>
        /// <param name="remoteEndPoint">Զ�������ʶ</param>
        /// <param name="sessionIdentifier">Session��ʶ��</param>
        public UdpPipeChannel(Socket socket, IPipelineFilter<TPackageInfo> pipelineFilter, ChannelOptions options, IPEndPoint remoteEndPoint, string sessionIdentifier)
            : base(pipelineFilter, options)
        {
            _socket = socket;
            _remoteEndPoint = remoteEndPoint;
            SessionIdentifier = sessionIdentifier;
        }

        public string SessionIdentifier { get; }
        /// <summary>
        /// �ر�
        /// </summary>
        protected override void Close()
        {
            WriteEOFPackage();
        }
        /// <summary>
        /// ���ݳ����ܵ�
        /// </summary>
        /// <param name="memory">�ڴ�</param>
        /// <param name="cancellationToken">�첽������ʶToken</param>
        /// <returns></returns>
        protected override ValueTask<int> FillPipeWithDataAsync(Memory<byte> memory, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="buffer">ֻ���ֽ�������</param>
        /// <param name="cancellationToken">�첽������ʶToken</param>
        /// <returns>�������ݵĳ���</returns>
        protected override async ValueTask<int> SendOverIOAsync(ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
        {
            var total = 0;

            foreach (var piece in buffer)
            {
                total += await _socket.SendToAsync(GetArrayByMemory<byte>(piece), SocketFlags.None, _remoteEndPoint);
            }

            return total;
        }
    }
}