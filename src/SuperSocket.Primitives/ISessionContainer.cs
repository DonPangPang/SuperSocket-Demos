using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSocket.Channel;

namespace SuperSocket
{
    public interface ISessionContainer
    {
        /// <summary>
        /// ͨ��ID��ȡSession
        /// </summary>
        /// <param name="sessionID">SessionID</param>
        /// <returns></returns>
        IAppSession GetSessionByID(string sessionID);
        /// <summary>
        /// ��ȡSession����
        /// </summary>
        /// <returns></returns>
        int GetSessionCount();
        /// <summary>
        /// ��ȡSession
        /// </summary>
        /// <param name="criteria">��׼</param>
        /// <returns></returns>
        IEnumerable<IAppSession> GetSessions(Predicate<IAppSession> criteria = null);
        /// <summary>
        /// ��ȡSession
        /// </summary>
        /// <typeparam name="TAppSession">Session����</typeparam>
        /// <param name="criteria">��׼</param>
        /// <returns></returns>
        IEnumerable<TAppSession> GetSessions<TAppSession>(Predicate<TAppSession> criteria = null)
            where TAppSession : IAppSession;
    }

    public interface IAsyncSessionContainer
    {
        /// <summary>
        /// �첽ͨ��ID��ȡSession
        /// </summary>
        /// <param name="sessionID">Session��ID</param>
        /// <returns>Session����</returns>
        ValueTask<IAppSession> GetSessionByIDAsync(string sessionID);
        /// <summary>
        /// �첽��ȡSession����
        /// </summary>
        /// <returns>Session����</returns>
        ValueTask<int> GetSessionCountAsync();
        /// <summary>
        /// �첽��ȡSession
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        ValueTask<IEnumerable<IAppSession>> GetSessionsAsync(Predicate<IAppSession> criteria = null);
        /// <summary>
        /// �첽��ȡSession
        /// </summary>
        /// <typeparam name="TAppSession"></typeparam>
        /// <param name="criteria"></param>
        /// <returns></returns>
        ValueTask<IEnumerable<TAppSession>> GetSessionsAsync<TAppSession>(Predicate<TAppSession> criteria = null)
            where TAppSession : IAppSession;
    }
}