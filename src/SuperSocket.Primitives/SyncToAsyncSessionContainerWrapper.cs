using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSocket.Channel;

namespace SuperSocket
{
    public class SyncToAsyncSessionContainerWrapper : IAsyncSessionContainer
    {
        ISessionContainer _syncSessionContainer;
        /// <summary>
        /// ��ʼ��Session����
        /// </summary>
        public ISessionContainer SessionContainer
        {
            get { return _syncSessionContainer; }
        }
        /// <summary>
        /// ͬ��ת�첽Session������װ
        /// </summary>
        /// <param name="syncSessionContainer"></param>
        public SyncToAsyncSessionContainerWrapper(ISessionContainer syncSessionContainer)
        {
            _syncSessionContainer = syncSessionContainer;
        }
        /// <summary>
        /// ʹ��ID�첽��ȡSession
        /// </summary>
        /// <param name="sessionID">Ҫ��ȡSession��ID</param>
        /// <returns>Sessionʵ��</returns>
        public ValueTask<IAppSession> GetSessionByIDAsync(string sessionID)
        {
            return new ValueTask<IAppSession>(_syncSessionContainer.GetSessionByID(sessionID));
        }
        /// <summary>
        /// �첽��ȡSession������
        /// </summary>
        /// <returns>Session������</returns>
        public ValueTask<int> GetSessionCountAsync()
        {
            return new ValueTask<int>(_syncSessionContainer.GetSessionCount());
        }
        /// <summary>
        /// �첽��ȡSession
        /// </summary>
        /// <param name="criteria">��׼</param>
        /// <returns></returns>
        public ValueTask<IEnumerable<IAppSession>> GetSessionsAsync(Predicate<IAppSession> criteria = null)
        {
            return new ValueTask<IEnumerable<IAppSession>>(_syncSessionContainer.GetSessions(criteria));
        }
        /// <summary>
        /// �첽��ȡSession
        /// </summary>
        /// <typeparam name="TAppSession">Session������</typeparam>
        /// <param name="criteria">��׼</param>
        /// <returns></returns>
        public ValueTask<IEnumerable<TAppSession>> GetSessionsAsync<TAppSession>(Predicate<TAppSession> criteria = null) where TAppSession : IAppSession
        {
            return new ValueTask<IEnumerable<TAppSession>>(_syncSessionContainer.GetSessions<TAppSession>(criteria));
        }
    }
}