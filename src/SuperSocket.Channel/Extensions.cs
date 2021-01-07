using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SuperSocket.Channel
{
    public static class Extensions
    {
        /// <summary>
        /// ��ȡ���ݰ�Stream���첽��
        /// </summary>
        /// <typeparam name="TPackageInfo">����Ϣ</typeparam>
        /// <param name="channel">�ܵ�</param>
        /// <returns>���ݰ���</returns>
        public static IAsyncEnumerator<TPackageInfo> GetPackageStream<TPackageInfo>(this IChannel<TPackageInfo> channel)
            where TPackageInfo : class
        {
            return channel.RunAsync().GetAsyncEnumerator();
        }
        /// <summary>
        /// �첽����
        /// </summary>
        /// <typeparam name="TPackageInfo">����Ϣ</typeparam>
        /// <param name="packageStream">���ݰ�Stream</param>
        /// <returns>���ݰ���Ϣ</returns>
        public static async ValueTask<TPackageInfo> ReceiveAsync<TPackageInfo>(this IAsyncEnumerator<TPackageInfo> packageStream)
        {
            if (await packageStream.MoveNextAsync())
                return packageStream.Current;

            return default(TPackageInfo);
        }
    }
}
