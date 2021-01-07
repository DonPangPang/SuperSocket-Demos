
using System.Threading.Tasks;

namespace SuperSocket
{
    public abstract class MiddlewareBase : IMiddleware
    {
        /// <summary>
        /// �м���б�
        /// </summary>
        public int Order { get; protected set; } = 0;
        /// <summary>
        /// ��ʼ
        /// </summary>
        /// <param name="server">������</param>
        public virtual void Start(IServer server)
        {

        }
        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="server">������</param>
        public virtual void Shutdown(IServer server)
        {
            
        }
        /// <summary>
        /// ע��Session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public virtual ValueTask<bool> RegisterSession(IAppSession session)
        {
            return new ValueTask<bool>(true);
        }
    }
}