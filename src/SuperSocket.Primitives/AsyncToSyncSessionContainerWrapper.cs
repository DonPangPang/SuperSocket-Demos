using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSocket.Channel;

namespace SuperSocket
{
    public class AsyncToSyncSessionContainerWrapper : ISessionContainer
    {
        IAsyncSessionContainer _asyncSessionContainer;
        /// <summary>
        /// �첽תͬ��Session������װ
        /// </summary>
        /// <param name="asyncSessionContainer">�첽Session����</param>
        public AsyncToSyncSessionContainerWrapper(IAsyncSessionContainer asyncSessionContainer)
        {
            _asyncSessionContainer = asyncSessionContainer;
        }
        /// <summary>
        /// ͨ��ID��ȡSession
        /// </summary>
        /// <param name="sessionID">Ҫ��ȡ��SessionID</param>
        /// <returns></returns>
        public IAppSession GetSessionByID(string sessionID)
        {
            return _asyncSessionContainer.GetSessionByIDAsync(sessionID).Result;
        }
        /// <summary>
        /// ��ȡSession����
        /// </summary>
        /// <returns>Session�ĸ���</returns>
        public int GetSessionCount()
        {
            return _asyncSessionContainer.GetSessionCountAsync().Result;
        }
        /// <summary>
        /// ��ȡSession
        /// </summary>
        /// <param name="criteria">��׼</param>
        /// <returns></returns>
        public IEnumerable<IAppSession> GetSessions(Predicate<IAppSession> criteria)
        {
            return _asyncSessionContainer.GetSessionsAsync(criteria).Result;
        }
        /// <summary>
        /// ��ȡSession
        /// </summary>
        /// <typeparam name="TAppSession">Session</typeparam>
        /// <param name="criteria">��׼</param>
        /// <returns></returns>
        public IEnumerable<TAppSession> GetSessions<TAppSession>(Predicate<TAppSession> criteria) where TAppSession : IAppSession
        {
            return _asyncSessionContainer.GetSessionsAsync<TAppSession>(criteria).Result;
        }
    }
}