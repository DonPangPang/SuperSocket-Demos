using System;
using System.Threading.Tasks;

namespace SuperSocket.WebSocket.Server
{
    public class HandshakeOptions
    {
        /// <summary>
        /// Handshake queue checking interval, in seconds
        /// ���ֶ��м����������Ϊ��λ
        /// </summary>
        /// <value>default: 60</value>
        public int CheckingInterval { get; set; } = 60;

        /// <summary>
        /// Open handshake timeout, in seconds
        /// �����ֳ�ʱ������Ϊ��λ
        /// </summary>
        /// <value>default: 120</value>
        public int OpenHandshakeTimeOut { get; set; } = 120;

        /// <summary>
        /// Close handshake timeout, in seconds
        /// �ر����ֳ�ʱ������Ϊ��λ
        /// </summary>
        /// <value>default: 120</value>
        public int CloseHandshakeTimeOut { get; set; } = 120;

        /// <summary>
        /// ������֤��
        /// </summary>
        public Func<WebSocketSession, WebSocketPackage, ValueTask<bool>> HandshakeValidator { get; set; }
    }
}