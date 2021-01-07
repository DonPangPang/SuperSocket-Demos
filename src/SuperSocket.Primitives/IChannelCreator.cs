using System.Threading.Tasks;
using SuperSocket.Channel;

namespace SuperSocket
{
    public delegate void NewClientAcceptHandler(IChannelCreator listener, IChannel channel);

    public interface IChannelCreator
    {
        /// <summary>
        /// ��������
        /// </summary>
        ListenOptions Options { get; }
        /// <summary>
        /// ��ʼ
        /// </summary>
        /// <returns></returns>
        bool Start();
        /// <summary>
        /// �ͻ���Ӧ����
        /// </summary>
        event NewClientAcceptHandler NewClientAccepted;
        /// <summary>
        /// ����ͨ��
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        Task<IChannel> CreateChannel(object connection);
        /// <summary>
        /// �첽ֹͣ
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
        /// <summary>
        /// ͨ������״̬
        /// </summary>
        bool IsRunning { get; }
    }
}