using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.ProtoBase;
using SuperSocket.Server;

namespace SuperSocket
{
    public class SuperSocketHostBuilder<TReceivePackage> : HostBuilderAdapter<SuperSocketHostBuilder<TReceivePackage>>, ISuperSocketHostBuilder<TReceivePackage>, IHostBuilder
    {
        private Func<HostBuilderContext, IConfiguration, IConfiguration> _serverOptionsReader;

        protected List<Action<HostBuilderContext, IServiceCollection>> ConfigureServicesActions { get; private set; } = new List<Action<HostBuilderContext, IServiceCollection>>();

        protected List<Action<HostBuilderContext, IServiceCollection>> ConfigureSupplementServicesActions = new List<Action<HostBuilderContext, IServiceCollection>>();
        /// <summary>
        /// ��ʼ������������
        /// </summary>
        /// <param name="hostBuilder"></param>
        public SuperSocketHostBuilder(IHostBuilder hostBuilder)
            : base(hostBuilder)
        {

        }
        /// <summary>
        /// ��ʼ������������
        /// </summary>
        public SuperSocketHostBuilder()
            : this(args: null)
        {

        }
        /// <summary>
        /// ��ʼ������������
        /// </summary>
        /// <param name="args">����</param>
        public SuperSocketHostBuilder(string[] args)
            : base(args)
        {

        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public override IHost Build()
        {
            return HostBuilder.ConfigureServices((ctx, services) =>
            {
                RegisterBasicServices(ctx, services, services);
            }).ConfigureServices((ctx, services) =>
            {
                foreach (var action in ConfigureServicesActions)
                {
                    action(ctx, services);
                }

                foreach (var action in ConfigureSupplementServicesActions)
                {
                    action(ctx, services);
                }
            }).ConfigureServices((ctx, services) =>
            {
                RegisterDefaultServices(ctx, services, services);
            }).Build();
        }
        /// <summary>
        /// ���ò������
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        public ISuperSocketHostBuilder<TReceivePackage> ConfigureSupplementServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            ConfigureSupplementServicesActions.Add(configureDelegate);
            return this;
        }
        /// <summary>
        /// ���ò������
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        ISuperSocketHostBuilder ISuperSocketHostBuilder.ConfigureSupplementServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            return ConfigureSupplementServices(configureDelegate);
        }
        /// <summary>
        /// ע���������
        /// </summary>
        /// <param name="builderContext">������������</param>
        /// <param name="servicesInHost">��������</param>
        /// <param name="services">����</param>
        protected virtual void RegisterBasicServices(HostBuilderContext builderContext, IServiceCollection servicesInHost, IServiceCollection services)
        {
            var serverOptionReader = _serverOptionsReader;

            if (serverOptionReader == null)
            {
                serverOptionReader = (ctx, config) =>
                {
                    return config;
                };
            }

            services.AddOptions();

            var config = builderContext.Configuration.GetSection("serverOptions");
            var serverConfig = serverOptionReader(builderContext, config);

            services.Configure<ServerOptions>(serverConfig);
        }
        /// <summary>
        /// ע��Ĭ�Ϸ���
        /// </summary>
        /// <param name="builderContext">������������</param>
        /// <param name="servicesInHost">��������</param>
        /// <param name="services">����</param>
        protected virtual void RegisterDefaultServices(HostBuilderContext builderContext, IServiceCollection servicesInHost, IServiceCollection services)
        {
            // if the package type is StringPackageInfo
            if (typeof(TReceivePackage) == typeof(StringPackageInfo))
            {
                services.TryAdd(ServiceDescriptor.Singleton<IPackageDecoder<StringPackageInfo>, DefaultStringPackageDecoder>());
            }

            services.TryAdd(ServiceDescriptor.Singleton<IPackageEncoder<string>, DefaultStringEncoderForDI>());

            // if no host service was defined, just use the default one
            if (!CheckIfExistHostedService(services))
            {
                RegisterDefaultHostedService(servicesInHost);
            }
        }
        /// <summary>
        /// ����Ƿ������������
        /// </summary>
        /// <param name="services">����</param>
        /// <returns></returns>
        protected virtual bool CheckIfExistHostedService(IServiceCollection services)
        {
            return services.Any(s => s.ServiceType == typeof(IHostedService)
                && typeof(SuperSocketService<TReceivePackage>).IsAssignableFrom(GetImplementationType(s)));
        }
        /// <summary>
        /// ��ȡʵ������
        /// </summary>
        /// <param name="serviceDescriptor">����������</param>
        /// <returns></returns>
        private Type GetImplementationType(ServiceDescriptor serviceDescriptor)
        {
            if (serviceDescriptor.ImplementationType != null)
                return serviceDescriptor.ImplementationType;

            if (serviceDescriptor.ImplementationInstance != null)
                return serviceDescriptor.ImplementationInstance.GetType();

            if (serviceDescriptor.ImplementationFactory != null)
            {
                var typeArguments = serviceDescriptor.ImplementationFactory.GetType().GenericTypeArguments;

                if (typeArguments.Length == 2)
                    return typeArguments[1];
            }

            return null;
        }
        /// <summary>
        /// ע��Ĭ����������
        /// </summary>
        /// <param name="servicesInHost">��������</param>
        protected virtual void RegisterDefaultHostedService(IServiceCollection servicesInHost)
        {
            RegisterHostedService<SuperSocketService<TReceivePackage>>(servicesInHost);
        }
        /// <summary>
        /// ע����������
        /// </summary>
        /// <typeparam name="THostedService">�������������</typeparam>
        /// <param name="servicesInHost">��������</param>
        protected virtual void RegisterHostedService<THostedService>(IServiceCollection servicesInHost)
            where THostedService : class, IHostedService
        {
            servicesInHost.AddSingleton<THostedService, THostedService>();
            servicesInHost.AddSingleton<IServerInfo>(s => s.GetService<THostedService>() as IServerInfo);
            servicesInHost.AddHostedService<THostedService>(s => s.GetService<THostedService>());
        }
        /// <summary>
        /// ���÷���������
        /// </summary>
        /// <param name="serverOptionsReader"></param>
        /// <returns></returns>
        public ISuperSocketHostBuilder<TReceivePackage> ConfigureServerOptions(Func<HostBuilderContext, IConfiguration, IConfiguration> serverOptionsReader)
        {
            _serverOptionsReader = serverOptionsReader;
            return this;
        }
        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> ISuperSocketHostBuilder<TReceivePackage>.ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            return ConfigureServices(configureDelegate);
        }
        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        public override SuperSocketHostBuilder<TReceivePackage> ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            ConfigureServicesActions.Add(configureDelegate);
            return this;
        }
        /// <summary>
        /// ֻ�ùܵ�ɸѡ��
        /// </summary>
        /// <typeparam name="TPipelineFilter">�ܵ�ɸѡ��������</typeparam>
        /// <returns></returns>
        public virtual ISuperSocketHostBuilder<TReceivePackage> UsePipelineFilter<TPipelineFilter>()
            where TPipelineFilter : IPipelineFilter<TReceivePackage>, new()
        {
            return this.ConfigureServices((ctx, services) =>
            {
                services.AddSingleton<IPipelineFilterFactory<TReceivePackage>, DefaultPipelineFilterFactory<TReceivePackage, TPipelineFilter>>();
            });
        }
        /// <summary>
        /// ʹ�ùܵ�ɸѡ������
        /// </summary>
        /// <typeparam name="TPipelineFilterFactory">�ܵ�ɸѡ����������</typeparam>
        /// <returns></returns>
        public virtual ISuperSocketHostBuilder<TReceivePackage> UsePipelineFilterFactory<TPipelineFilterFactory>()
            where TPipelineFilterFactory : class, IPipelineFilterFactory<TReceivePackage>
        {
            return this.ConfigureServices((ctx, services) =>
            {
                services.AddSingleton<IPipelineFilterFactory<TReceivePackage>, TPipelineFilterFactory>();
            });
        }
        /// <summary>
        /// ʹ��Session
        /// </summary>
        /// <typeparam name="TSession"></typeparam>
        /// <returns></returns>
        public virtual ISuperSocketHostBuilder<TReceivePackage> UseSession<TSession>()
            where TSession : IAppSession
        {
            return this.UseSessionFactory<GenericSessionFactory<TSession>>();
        }
        /// <summary>
        /// ʹ��Session����
        /// </summary>
        /// <typeparam name="TSessionFactory"></typeparam>
        /// <returns></returns>
        public virtual ISuperSocketHostBuilder<TReceivePackage> UseSessionFactory<TSessionFactory>()
            where TSessionFactory : class, ISessionFactory
        {
            return this.ConfigureServices(
                (hostCtx, services) =>
                {
                    services.AddSingleton<ISessionFactory, TSessionFactory>();
                }
            );
        }
        /// <summary>
        /// ʹ����������
        /// </summary>
        /// <typeparam name="THostedService"></typeparam>
        /// <returns></returns>
        public virtual ISuperSocketHostBuilder<TReceivePackage> UseHostedService<THostedService>()
            where THostedService : class, IHostedService
        {
            if (!typeof(SuperSocketService<TReceivePackage>).IsAssignableFrom(typeof(THostedService)))
            {
                throw new ArgumentException($"The type parameter should be subclass of {nameof(SuperSocketService<TReceivePackage>)}", nameof(THostedService));
            }

            return this.ConfigureServices((ctx, services) =>
            {
                RegisterHostedService<THostedService>(services);
            });
        }

        /// <summary>
        /// ʹ�ð�������
        /// </summary>
        /// <typeparam name="TPackageDecoder">��������������</typeparam>
        /// <returns></returns>
        public virtual ISuperSocketHostBuilder<TReceivePackage> UsePackageDecoder<TPackageDecoder>()
            where TPackageDecoder : class, IPackageDecoder<TReceivePackage>
        {
            return this.ConfigureServices(
                (hostCtx, services) =>
                {
                    services.AddSingleton<IPackageDecoder<TReceivePackage>, TPackageDecoder>();
                }
            );
        }
        /// <summary>
        /// ʹ���м��
        /// </summary>
        /// <typeparam name="TMiddleware">�м��������</typeparam>
        /// <returns></returns>
        public virtual ISuperSocketHostBuilder<TReceivePackage> UseMiddleware<TMiddleware>()
            where TMiddleware : class, IMiddleware
        {
            return this.ConfigureServices((ctx, services) =>
            {
                services.TryAddEnumerable(ServiceDescriptor.Singleton<IMiddleware, TMiddleware>());
            });
        }
        /// <summary>
        /// ʹ�ð����������
        /// </summary>
        /// <typeparam name="TPackageHandlingScheduler">�����������������</typeparam>
        /// <returns></returns>
        public ISuperSocketHostBuilder<TReceivePackage> UsePackageHandlingScheduler<TPackageHandlingScheduler>()
            where TPackageHandlingScheduler : class, IPackageHandlingScheduler<TReceivePackage>
        {
            return this.ConfigureServices(
                (hostCtx, services) =>
                {
                    services.AddSingleton<IPackageHandlingScheduler<TReceivePackage>, TPackageHandlingScheduler>();
                }
            );
        }
        /// <summary>
        /// ʹ�ð����������ķ�����
        /// </summary>
        /// <returns></returns>
        public ISuperSocketHostBuilder<TReceivePackage> UsePackageHandlingContextAccessor()
        {
            return this.ConfigureServices(
                 (hostCtx, services) =>
                 {
                     services.AddSingleton<IPackageHandlingContextAccessor<TReceivePackage>, PackageHandlingContextAccessor<TReceivePackage>>();
                 }
             );
        }
    }

    public static class SuperSocketHostBuilder
    {
        /// <summary>
        /// ����SuperSocket����������
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> Create<TReceivePackage>()
            where TReceivePackage : class
        {
            return Create<TReceivePackage>(args: null);
        }
        /// <summary>
        /// ����SuperSocket����������
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <param name="args">����</param>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> Create<TReceivePackage>(string[] args)
        {
            return new SuperSocketHostBuilder<TReceivePackage>(args);
        }
        /// <summary>
        /// ����SuperSocket����������
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <typeparam name="TPipelineFilter">�ܵ�ɸѡ��</typeparam>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> Create<TReceivePackage, TPipelineFilter>()
            where TPipelineFilter : IPipelineFilter<TReceivePackage>, new()
        {
            return Create<TReceivePackage, TPipelineFilter>(args: null);
        }
        /// <summary>
        /// ����SuperSocket����������
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <typeparam name="TPipelineFilter">�ܵ�ɸѡ��</typeparam>
        /// <param name="args">����</param>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> Create<TReceivePackage, TPipelineFilter>(string[] args)
            where TPipelineFilter : IPipelineFilter<TReceivePackage>, new()
        {
            return new SuperSocketHostBuilder<TReceivePackage>(args)
                .UsePipelineFilter<TPipelineFilter>();
        }
    }
}
