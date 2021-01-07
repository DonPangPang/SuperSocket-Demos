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
    public class MultipleServerHostBuilder : HostBuilderAdapter<MultipleServerHostBuilder>
    {
        private List<IServerHostBuilderAdapter> _hostBuilderAdapters = new List<IServerHostBuilderAdapter>();
        /// <summary>
        /// ��ʼ�������������������
        /// </summary>
        private MultipleServerHostBuilder()
            : this(args: null)
        {

        }
        /// <summary>
        /// ��ʼ�������������������
        /// </summary>
        /// <param name="args">����</param>
        private MultipleServerHostBuilder(string[] args)
            : base(args)
        {

        }
        /// <summary>
        /// ��ʼ�������������������
        /// </summary>
        /// <param name="hostBuilder">Host Builder</param>
        internal MultipleServerHostBuilder(IHostBuilder hostBuilder)
            : base(hostBuilder)
        {

        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="context">��������������</param>
        /// <param name="hostServices">��������</param>
        protected virtual void ConfigureServers(HostBuilderContext context, IServiceCollection hostServices)
        {
            foreach (var adapter in _hostBuilderAdapters)
            {
                adapter.ConfigureServer(context, hostServices);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public override IHost Build()
        {
            this.ConfigureServices(ConfigureServers);

            var host = base.Build();
            var services = host.Services;

            foreach (var adapter in _hostBuilderAdapters)
            {
                adapter.ConfigureServiceProvider(services);
            }
            
            return host;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public static MultipleServerHostBuilder Create()
        {
            return Create(args: null);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="args">����</param>
        /// <returns></returns>
        public static MultipleServerHostBuilder Create(string[] args)
        {
            return new MultipleServerHostBuilder(args);
        }
        /// <summary>
        /// ��������������������
        /// </summary>
        /// <typeparam name="TReceivePackage"></typeparam>
        /// <param name="hostBuilderDelegate"></param>
        /// <returns></returns>
        private ServerHostBuilderAdapter<TReceivePackage> CreateServerHostBuilder<TReceivePackage>(Action<SuperSocketHostBuilder<TReceivePackage>> hostBuilderDelegate)
            where TReceivePackage : class
        {
            var hostBuilder = new ServerHostBuilderAdapter<TReceivePackage>(this);            
            hostBuilderDelegate(hostBuilder);
            return hostBuilder;
        }
        /// <summary>
        /// ��ӷ�����
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <param name="hostBuilderDelegate">��������ί��</param>
        /// <returns></returns>
        public MultipleServerHostBuilder AddServer<TReceivePackage>(Action<SuperSocketHostBuilder<TReceivePackage>> hostBuilderDelegate)
            where TReceivePackage : class
        {
            var hostBuilder = CreateServerHostBuilder<TReceivePackage>(hostBuilderDelegate);
            _hostBuilderAdapters.Add(hostBuilder);
            return this;
        }
        /// <summary>
        /// ��ӷ�����
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <typeparam name="TPipelineFilter">�ܵ�ɸѡ��</typeparam>
        /// <param name="hostBuilderDelegate">��������ί��</param>
        /// <returns></returns>
        public MultipleServerHostBuilder AddServer<TReceivePackage, TPipelineFilter>(Action<SuperSocketHostBuilder<TReceivePackage>> hostBuilderDelegate)
            where TReceivePackage : class
            where TPipelineFilter : IPipelineFilter<TReceivePackage>, new()
        {            
            var hostBuilder = CreateServerHostBuilder<TReceivePackage>(hostBuilderDelegate);
            _hostBuilderAdapters.Add(hostBuilder);
            hostBuilder.UsePipelineFilter<TPipelineFilter>();
            return this;
        }
        /// <summary>
        /// ��ӷ�����
        /// </summary>
        /// <param name="hostBuilderAdapter">��������������</param>
        /// <returns></returns>
        public MultipleServerHostBuilder AddServer(IServerHostBuilderAdapter hostBuilderAdapter)
        {            
            _hostBuilderAdapters.Add(hostBuilderAdapter);
            return this;
        }
        /// <summary>
        /// ��ӷ�����
        /// </summary>
        /// <typeparam name="TSuperSocketService">SuperSocket����</typeparam>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <typeparam name="TPipelineFilter">�ܵ�ɸѡ��</typeparam>
        /// <param name="hostBuilderDelegate">��������ί��</param>
        /// <returns></returns>
        public MultipleServerHostBuilder AddServer<TSuperSocketService, TReceivePackage, TPipelineFilter>(Action<SuperSocketHostBuilder<TReceivePackage>> hostBuilderDelegate)
            where TReceivePackage : class
            where TPipelineFilter : IPipelineFilter<TReceivePackage>, new()
            where TSuperSocketService : SuperSocketService<TReceivePackage>
        {
            var hostBuilder = CreateServerHostBuilder<TReceivePackage>(hostBuilderDelegate);

            _hostBuilderAdapters.Add(hostBuilder);

            hostBuilder
                .UsePipelineFilter<TPipelineFilter>()
                .UseHostedService<TSuperSocketService>();
            return this;
        }
    }
}