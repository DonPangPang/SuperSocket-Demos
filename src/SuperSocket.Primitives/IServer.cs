using System;
using System.Threading.Tasks;

namespace SuperSocket
{
    public interface IServer : IServerInfo, IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// �첽����
        /// </summary>
        /// <returns></returns>
        Task<bool> StartAsync();
        /// <summary>
        /// �첽ֹͣ
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
    }
}