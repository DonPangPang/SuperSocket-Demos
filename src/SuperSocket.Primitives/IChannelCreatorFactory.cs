using Microsoft.Extensions.Logging;
using SuperSocket.Channel;

namespace SuperSocket
{
    public interface IChannelCreatorFactory
    {
        /// <summary>
        /// ����ͨ��������
        /// </summary>
        /// <typeparam name="TPackageInfo">����Ϣ</typeparam>
        /// <param name="options">��������</param>
        /// <param name="channelOptions">ͨ������</param>
        /// <param name="loggerFactory">��־����</param>
        /// <param name="pipelineFilterFactory">�ܵ�ɸѡ������</param>
        /// <returns></returns>
        IChannelCreator CreateChannelCreator<TPackageInfo>(ListenOptions options, ChannelOptions channelOptions, ILoggerFactory loggerFactory, object pipelineFilterFactory);
    }
}