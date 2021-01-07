using System;

namespace SuperSocket
{
    public interface IServerInfo
    {
        /// <summary>
        /// ����
        /// </summary>
        string Name { get; }
        /// <summary>
        /// ����������
        /// </summary>
        object DataContext { get; set; }
        /// <summary>
        /// Session����
        /// </summary>
        int SessionCount { get; }
        /// <summary>
        /// ServiceProvider
        /// </summary>
        IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// ������״̬
        /// </summary>
        ServerState State { get; }
    }
}