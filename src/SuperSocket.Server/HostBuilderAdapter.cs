using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperSocket.Channel;
using SuperSocket.ProtoBase;

namespace SuperSocket.Server
{
    public abstract class HostBuilderAdapter<THostBuilder> : IHostBuilder
        where THostBuilder : HostBuilderAdapter<THostBuilder>
    {
        protected IHostBuilder HostBuilder { get; private set; }
        /// <summary>
        /// ��ʼ��Host Builder������
        /// </summary>
        public HostBuilderAdapter()
            : this(args: null)
        {
            
        }
        /// <summary>
        /// ��ʼ��Host Builder������
        /// </summary>
        /// <param name="args">����</param>
        public HostBuilderAdapter(string[] args)
            : this(Host.CreateDefaultBuilder(args))
        {
            
        }
        /// <summary>
        /// ��ʼ��Host Builder������
        /// </summary>
        /// <param name="hostBuilder">Host Builder</param>
        public HostBuilderAdapter(IHostBuilder hostBuilder)
        {
            HostBuilder = hostBuilder;
        }

        public IDictionary<object, object> Properties => HostBuilder.Properties;
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public virtual IHost Build()
        {
            return HostBuilder.Build();
        }
        /// <summary>
        /// ����Ӧ�ó�������
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        IHostBuilder IHostBuilder.ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            return ConfigureAppConfiguration(configureDelegate);
        }
        /// <summary>
        /// ����Ӧ�ó�������
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        public virtual THostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            HostBuilder.ConfigureAppConfiguration(configureDelegate);
            return this as THostBuilder;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <typeparam name="TContainerBuilder">����������������</typeparam>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        IHostBuilder IHostBuilder.ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            return ConfigureContainer<TContainerBuilder>(configureDelegate);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <typeparam name="TContainerBuilder">����������������</typeparam>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        public virtual THostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            HostBuilder.ConfigureContainer<TContainerBuilder>(configureDelegate);
            return this as THostBuilder;
        }
        /// <summary>
        /// ����Host������
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        IHostBuilder IHostBuilder.ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            return ConfigureHostConfiguration(configureDelegate);
        }
        /// <summary>
        /// ����Host������
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        public THostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            HostBuilder.ConfigureHostConfiguration(configureDelegate);
            return this as THostBuilder;
        }
        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        IHostBuilder IHostBuilder.ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            return ConfigureServices(configureDelegate);
        }
        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        public virtual THostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            HostBuilder.ConfigureServices(configureDelegate);
            return this as THostBuilder;
        }
        /// <summary>
        /// ʹ�÷���Ӧ����
        /// </summary>
        /// <typeparam name="TContainerBuilder">����������</typeparam>
        /// <param name="factory">�����ṩ����</param>
        /// <returns></returns>
        IHostBuilder IHostBuilder.UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            return UseServiceProviderFactory<TContainerBuilder>(factory);
        }
        /// <summary>
        /// ʹ�÷����ṩ����
        /// </summary>
        /// <typeparam name="TContainerBuilder">����������</typeparam>
        /// <param name="factory">����������</param>
        /// <returns></returns>
        public virtual THostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            HostBuilder.UseServiceProviderFactory<TContainerBuilder>(factory);
            return this as THostBuilder;
        }
        /// <summary>
        /// ʹ�÷����ṩ����
        /// </summary>
        /// <typeparam name="TContainerBuilder">����������</typeparam>
        /// <param name="factory">����������</param>
        /// <returns></returns>
        IHostBuilder IHostBuilder.UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            return UseServiceProviderFactory<TContainerBuilder>(factory);
        }
        /// <summary>
        /// ʹ�÷����ṩ����
        /// </summary>
        /// <typeparam name="TContainerBuilder">����������</typeparam>
        /// <param name="factory">����������</param>
        /// <returns></returns>
        public virtual THostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            HostBuilder.UseServiceProviderFactory<TContainerBuilder>(factory);
            return this as THostBuilder;
        }
    }
}