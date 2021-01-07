using System;
using Microsoft.Extensions.Hosting;

namespace SuperSocket.Server
{
    internal interface IConfigureContainerAdapter
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="hostContext">����������</param>
        /// <param name="containerBuilder">����������</param>
        void ConfigureContainer(HostBuilderContext hostContext, object containerBuilder);
    }

    internal class ConfigureContainerAdapter<TContainerBuilder> : IConfigureContainerAdapter
    {
        private Action<HostBuilderContext, TContainerBuilder> _action;
        /// <summary>
        /// ��ʼ����������������
        /// </summary>
        /// <param name="action"></param>
        public ConfigureContainerAdapter(Action<HostBuilderContext, TContainerBuilder> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="hostContext">����������</param>
        /// <param name="containerBuilder">����������</param>
        public void ConfigureContainer(HostBuilderContext hostContext, object containerBuilder)
        {
            _action(hostContext, (TContainerBuilder)containerBuilder);
        }
    }
}