using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SuperSocket.ProtoBase;
using SuperSocket.Server;
using SuperSocket.Channel;

namespace SuperSocket
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// ��ΪSuperSocket Host Builder
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <param name="hostBuilder">Host Builder</param>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> AsSuperSocketHostBuilder<TReceivePackage>(this IHostBuilder hostBuilder)
        {
            if (hostBuilder is ISuperSocketHostBuilder<TReceivePackage> ssHostBuilder)
            {
                return ssHostBuilder;
            }

            return new SuperSocketHostBuilder<TReceivePackage>(hostBuilder);
        }
        /// <summary>
        /// ��ΪSuperSocket Host Builder
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <typeparam name="TPipelineFilter">�ܵ�ɸѡ��</typeparam>
        /// <param name="hostBuilder">Host Builder</param>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> AsSuperSocketHostBuilder<TReceivePackage, TPipelineFilter>(this IHostBuilder hostBuilder)
            where TPipelineFilter : IPipelineFilter<TReceivePackage>, new()
        {
            if (hostBuilder is ISuperSocketHostBuilder<TReceivePackage> ssHostBuilder)
            {
                return ssHostBuilder;
            }

            return (new SuperSocketHostBuilder<TReceivePackage>(hostBuilder))
                .UsePipelineFilter<TPipelineFilter>();
        }
        /// <summary>
        /// ʹ�ùܵ�ɸѡ������
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <param name="hostBuilder">Host Builder</param>
        /// <param name="filterFactory">�ܵ�ɸѡ��</param>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> UsePipelineFilterFactory<TReceivePackage>(this ISuperSocketHostBuilder<TReceivePackage> hostBuilder, Func<object, IPipelineFilter<TReceivePackage>> filterFactory)
        {
            hostBuilder.ConfigureServices(
                (hostCtx, services) =>
                {
                    services.AddSingleton<Func<object, IPipelineFilter<TReceivePackage>>>(filterFactory);
                }
            );

            return hostBuilder.UsePipelineFilterFactory<DelegatePipelineFilterFactory<TReceivePackage>>();
        }
        /// <summary>
        /// ʹ���������Session
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <param name="hostBuilder">Host Builder</param>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> UseClearIdleSession<TReceivePackage>(this ISuperSocketHostBuilder<TReceivePackage> hostBuilder)
        {
            return hostBuilder.UseMiddleware<ClearIdleSessionMiddleware>();
        }
        /// <summary>
        /// ʹ��Session����
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <param name="hostBuilder">Host Builder</param>
        /// <param name="onConnected">���Ӻ�</param>
        /// <param name="onClosed">�رպ�</param>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> UseSessionHandler<TReceivePackage>(this ISuperSocketHostBuilder<TReceivePackage> hostBuilder, Func<IAppSession, ValueTask> onConnected = null, Func<IAppSession, CloseEventArgs, ValueTask> onClosed = null)
        {
            return hostBuilder.ConfigureServices(
                (hostCtx, services) =>
                {
                    services.AddSingleton<SessionHandlers>(new SessionHandlers
                    {
                        Connected = onConnected,
                        Closed = onClosed
                    });
                }
            );
        }        
        /// <summary>
        /// ����SuperSocket
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <param name="hostBuilder">Host Buider</param>
        /// <param name="configurator">������</param>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> ConfigureSuperSocket<TReceivePackage>(this ISuperSocketHostBuilder<TReceivePackage> hostBuilder, Action<ServerOptions> configurator)
        {
            return hostBuilder.ConfigureServices(
                (hostCtx, services) =>
                {
                    services.Configure<ServerOptions>(configurator);
                }
            );
        }
        /// <summary>
        /// ����Socketѡ��
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <param name="hostBuilder">Host Builder</param>
        /// <param name="socketOptionsSetter">Socketѡ��������</param>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> ConfigureSocketOptions<TReceivePackage>(this ISuperSocketHostBuilder<TReceivePackage> hostBuilder, Action<Socket> socketOptionsSetter)
            where TReceivePackage : class
        {
            return hostBuilder.ConfigureServices(
                (hostCtx, services) =>
                {
                    services.AddSingleton<SocketOptionsSetter>(new SocketOptionsSetter(socketOptionsSetter));
                }
            );
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="hostBuilder">Host Builder</param>
        /// <returns></returns>
        public static IServer BuildAsServer(this IHostBuilder hostBuilder)
        {
            var host = hostBuilder.Build();
            return host.AsServer();
        }
        /// <summary>
        /// ��Ϊ������
        /// </summary>
        /// <param name="host">����</param>
        /// <returns></returns>
        public static IServer AsServer(this IHost host)
        {
            return host.Services.GetService<IEnumerable<IHostedService>>().OfType<IServer>().FirstOrDefault();
        }
        /// <summary>
        /// ���ô�����
        /// </summary>
        /// <typeparam name="TReceivePackage">���ܰ�������</typeparam>
        /// <param name="hostBuilder">Host Builder</param>
        /// <param name="errorHandler">������</param>
        /// <returns></returns>
        public static ISuperSocketHostBuilder<TReceivePackage> ConfigureErrorHandler<TReceivePackage>(this ISuperSocketHostBuilder<TReceivePackage> hostBuilder, Func<IAppSession, PackageHandlingException<TReceivePackage>, ValueTask<bool>> errorHandler)
        {
            return hostBuilder.ConfigureServices(
                (hostCtx, services) =>
                {
                    services.AddSingleton<Func<IAppSession, PackageHandlingException<TReceivePackage>, ValueTask<bool>>>(errorHandler);
                }
            );
        }
        /// <summary>
        /// ʹ�ð�����
        /// </summary>
        /// <typeparam name="TReceivePackage">���հ�������</typeparam>
        /// <param name="hostBuilder">Host Builder</param>
        /// <param name="packageHandler">������</param>
        /// <param name="errorHandler">������</param>
        /// <returns></returns>
        // move to extensions
        public static ISuperSocketHostBuilder<TReceivePackage> UsePackageHandler<TReceivePackage>(this ISuperSocketHostBuilder<TReceivePackage> hostBuilder, Func<IAppSession, TReceivePackage, ValueTask> packageHandler, Func<IAppSession, PackageHandlingException<TReceivePackage>, ValueTask<bool>> errorHandler = null)
        {
            return hostBuilder.ConfigureServices(
                (hostCtx, services) =>
                {
                    if (packageHandler != null)
                        services.AddSingleton<IPackageHandler<TReceivePackage>>(new DelegatePackageHandler<TReceivePackage>(packageHandler));

                    if (errorHandler != null)
                        services.AddSingleton<Func<IAppSession, PackageHandlingException<TReceivePackage>, ValueTask<bool>>>(errorHandler);
                }
            );
        }
        /// <summary>
        /// ��Ϊ���������������
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static MultipleServerHostBuilder AsMultipleServerHostBuilder(this IHostBuilder hostBuilder)
        {
            return new MultipleServerHostBuilder(hostBuilder);
        }
    }
}
