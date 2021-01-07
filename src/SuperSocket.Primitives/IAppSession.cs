using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SuperSocket.Channel;
using SuperSocket.ProtoBase;

namespace SuperSocket
{
    public interface IAppSession
    {
        /// <summary>
        /// SessionID
        /// </summary>
        string SessionID { get; }
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        DateTimeOffset StartTime { get; }
        /// <summary>
        /// ���һ�λʱ��
        /// </summary>
        DateTimeOffset LastActiveTime { get; }
        /// <summary>
        /// ͨ��
        /// </summary>
        IChannel Channel { get; }
        /// <summary>
        /// Զ�������ʶ
        /// </summary>
        EndPoint RemoteEndPoint { get; }
        /// <summary>
        /// ���������ʶ
        /// </summary>
        EndPoint LocalEndPoint { get; }
        /// <summary>
        /// �첽����
        /// </summary>
        /// <param name="data">����</param>
        /// <returns></returns>
        ValueTask SendAsync(ReadOnlyMemory<byte> data);
        /// <summary>
        /// �첽����
        /// </summary>
        /// <typeparam name="TPackage">������</typeparam>
        /// <param name="packageEncoder">������</param>
        /// <param name="package">���ݰ�</param>
        /// <returns></returns>
        ValueTask SendAsync<TPackage>(IPackageEncoder<TPackage> packageEncoder, TPackage package);
        /// <summary>
        /// �첽�ر�
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        ValueTask CloseAsync(CloseReason reason);
        /// <summary>
        /// ������
        /// </summary>
        IServerInfo Server { get; }
        /// <summary>
        /// �첽���Ӵ���
        /// </summary>
        event AsyncEventHandler Connected;
        /// <summary>
        /// �첽�رմ���
        /// </summary>
        event AsyncEventHandler<CloseEventArgs> Closed;
        /// <summary>
        /// ����������
        /// </summary>
        object DataContext { get; set; }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="server">������</param>
        /// <param name="channel">ͨ��</param>
        void Initialize(IServerInfo server, IChannel channel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object this[object name] { get; set; }
        /// <summary>
        /// Session״̬
        /// </summary>
        SessionState State { get; }
        /// <summary>
        /// ����
        /// </summary>
        void Reset();
    }
}