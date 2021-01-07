
using System;
using Microsoft.Extensions.Logging;
using SuperSocket.ProtoBase;
using SuperSocket.Channel;
using System.Threading.Tasks;

namespace SuperSocket
{
    public interface IChannelRegister
    {
        /// <summary>
        /// ע��ͨ��
        /// </summary>
        /// <param name="connection">����</param>
        /// <returns></returns>
        Task RegisterChannel(object connection);
    }
}