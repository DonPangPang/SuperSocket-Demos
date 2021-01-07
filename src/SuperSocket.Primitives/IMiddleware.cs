
using System.Threading.Tasks;

namespace SuperSocket
{
    public interface IMiddleware
    {
        /// <summary>
        /// �м���嵥
        /// </summary>
        int Order { get; }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="server"></param>
        void Start(IServer server);
        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="server"></param>
        void Shutdown(IServer server);
        /// <summary>
        /// ע��Session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        ValueTask<bool> RegisterSession(IAppSession session);
    }
}