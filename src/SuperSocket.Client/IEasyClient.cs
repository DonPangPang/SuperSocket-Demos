using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;

namespace SuperSocket.Client
{
    public interface IEasyClient<TReceivePackage, TSendPackage> : IEasyClient<TReceivePackage>
        where TReceivePackage : class
    {
        /// <summary>
        /// �첽��������
        /// </summary>
        /// <param name="package">���ݰ�</param>
        /// <returns></returns>
        ValueTask SendAsync(TSendPackage package);      
    }

    
    public interface IEasyClient<TReceivePackage>
        where TReceivePackage : class
    {
        /// <summary>
        /// �첽����
        /// </summary>
        /// <param name="remoteEndPoint">Զ�������ʶ</param>
        /// <param name="cancellationToken">�첽����Token</param>
        /// <returns>true:�ɹ���false:ʧ�ܣ�</returns>
        ValueTask<bool> ConnectAsync(EndPoint remoteEndPoint, CancellationToken cancellationToken = default);
        /// <summary>
        /// �첽����
        /// </summary>
        /// <returns></returns>
        ValueTask<TReceivePackage> ReceiveAsync();
        /// <summary>
        /// ���������ʶ
        /// </summary>
        IPEndPoint LocalEndPoint { get; set; }
        /// <summary>
        /// ��ȫѡ��
        /// </summary>
        SecurityOptions Security { get; set; }
        /// <summary>
        /// ��ʼ����
        /// </summary>
        void StartReceive();
        /// <summary>
        /// �첽����
        /// </summary>
        /// <param name="data">����</param>
        /// <returns></returns>
        ValueTask SendAsync(ReadOnlyMemory<byte> data);
        /// <summary>
        /// �첽����
        /// </summary>
        /// <typeparam name="TSendPackage">���ݰ�������</typeparam>
        /// <param name="packageEncoder">���ı���</param>
        /// <param name="package">���ݰ�</param>
        /// <returns></returns>
        ValueTask SendAsync<TSendPackage>(IPackageEncoder<TSendPackage> packageEncoder, TSendPackage package);

        event EventHandler Closed;

        event PackageHandler<TReceivePackage> PackageHandler;
        /// <summary>
        /// �첽�ر�
        /// </summary>
        /// <returns></returns>
        ValueTask CloseAsync();
    }
}