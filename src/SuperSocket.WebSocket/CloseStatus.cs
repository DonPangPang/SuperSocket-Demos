using System;
using System.Buffers;

namespace SuperSocket.WebSocket
{
    public class CloseStatus
    {
        /// <summary>
        /// �ر�ԭ��
        /// </summary>
        public CloseReason Reason { get; set; }
        /// <summary>
        /// ԭ��˵��
        /// </summary>
        public string ReasonText { get; set; }
        /// <summary>
        /// Զ������
        /// </summary>
        public bool RemoteInitiated{ get; set; }
    }
}
