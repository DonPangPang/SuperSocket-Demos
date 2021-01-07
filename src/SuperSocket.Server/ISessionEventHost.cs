using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SuperSocket.Channel;

namespace SuperSocket.Server
{
    public interface ISessionEventHost
    {
        /// <summary>
        /// ����Session�����¼�
        /// </summary>
        /// <param name="session">Session</param>
        /// <returns></returns>
        ValueTask HandleSessionConnectedEvent(AppSession session);
        /// <summary>
        /// ����Session�ر��¼�
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="reason">�ر�ԭ��</param>
        /// <returns></returns>
        ValueTask HandleSessionClosedEvent(AppSession session, CloseReason reason);
    }
}