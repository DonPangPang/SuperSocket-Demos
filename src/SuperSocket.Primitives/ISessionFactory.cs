using System;
using SuperSocket.Channel;

namespace SuperSocket
{
    public interface ISessionFactory
    {
        /// <summary>
        /// ����һ��Sessionʵ��
        /// </summary>
        /// <returns></returns>
        IAppSession Create();
        /// <summary>
        /// Session������
        /// </summary>
        Type SessionType { get; }
    }
}