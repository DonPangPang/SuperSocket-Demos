using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SuperSocket.Channel;
using SuperSocket.ProtoBase;

namespace SuperSocket.Channel
{
    /// <summary>
    /// ͨ��/�ܵ��ر�ԭ��ö��
    /// </summary>
    public enum CloseReason
    {
        /// <summary>
        /// The socket is closed for unknown reason
        /// ����δ֪������Socket�ر�
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Close for server shutdown
        /// ���ڷ������ػ�����Socket�ر�
        /// </summary>
        ServerShutdown = 1,

        /// <summary>
        /// The close behavior is initiated from the remote endpoing
        /// �رյ���Ϊ��Զ�̽ڵ㷢��
        /// </summary>
        RemoteClosing = 2,

        /// <summary>
        /// The close behavior is initiated from the local endpoing
        /// �ر���Ϊ�ɱ��ؽڵ㷢��
        /// </summary>
        LocalClosing = 3,

        /// <summary>
        /// Application error
        /// Ӧ�ô���
        /// </summary>
        ApplicationError = 4,

        /// <summary>
        /// The socket is closed for a socket error
        /// Socket����
        /// </summary>
        SocketError = 5,

        /// <summary>
        /// The socket is closed by server for timeout
        /// ���ڷ�������ʱ����Soctet�ر�
        /// </summary>
        TimeOut = 6,

        /// <summary>
        /// Protocol error 
        /// Э�����
        /// </summary>
        ProtocolError = 7,

        /// <summary>
        /// SuperSocket internal error
        /// �ڲ�����
        /// </summary>
        InternalError = 8,
    }

    public class CloseEventArgs : EventArgs
    {
        public CloseReason Reason { get; private set; }

        public CloseEventArgs(CloseReason reason)
        {
            Reason = reason;
        }
    }
}