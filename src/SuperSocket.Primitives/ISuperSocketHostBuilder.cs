using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperSocket.ProtoBase;

namespace SuperSocket
{
    public interface ISuperSocketHostBuilder : IHostBuilder
    {
        /// <summary>
        /// ���ò������
        /// </summary>
        /// <param name="configureDelegate">����ί��</param>
        /// <returns></returns>
        ISuperSocketHostBuilder ConfigureSupplementServices(Action<HostBuilderContext, IServiceCollection> configureDelegate);
    }

    public interface ISuperSocketHostBuilder<TReceivePackage> : ISuperSocketHostBuilder
    {
        /// <summary>
        /// ���÷���������
        /// </summary>
        /// <param name="serverOptionsReader"></param>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> ConfigureServerOptions(Func<HostBuilderContext, IConfiguration, IConfiguration> serverOptionsReader);
        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        new ISuperSocketHostBuilder<TReceivePackage> ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate);
        /// <summary>
        /// ���ò������
        /// </summary>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        new ISuperSocketHostBuilder<TReceivePackage> ConfigureSupplementServices(Action<HostBuilderContext, IServiceCollection> configureDelegate);
        /// <summary>
        /// ʹ���м��
        /// </summary>
        /// <typeparam name="TMiddleware">�м��������</typeparam>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> UseMiddleware<TMiddleware>()
            where TMiddleware : class, IMiddleware;
        /// <summary>
        /// ʹ�ùܵ�ɸѡ��
        /// </summary>
        /// <typeparam name="TPipelineFilter">�ܵ�ɸѡ��</typeparam>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> UsePipelineFilter<TPipelineFilter>()
            where TPipelineFilter : IPipelineFilter<TReceivePackage>, new();
        /// <summary>
        /// ʹ�ùܵ�ɸѡ������
        /// </summary>
        /// <typeparam name="TPipelineFilterFactory">�ܵ�ɸѡ������</typeparam>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> UsePipelineFilterFactory<TPipelineFilterFactory>()
            where TPipelineFilterFactory : class, IPipelineFilterFactory<TReceivePackage>;
        /// <summary>
        /// ʹ��Host����
        /// </summary>
        /// <typeparam name="THostedService"></typeparam>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> UseHostedService<THostedService>()
            where THostedService : class, IHostedService;
        /// <summary>
        /// ʹ�ð�����
        /// </summary>
        /// <typeparam name="TPackageDecoder">����������</typeparam>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> UsePackageDecoder<TPackageDecoder>()
            where TPackageDecoder : class, IPackageDecoder<TReceivePackage>;
        /// <summary>
        /// ʹ�ð��������
        /// </summary>
        /// <typeparam name="TPackageHandlingScheduler"></typeparam>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> UsePackageHandlingScheduler<TPackageHandlingScheduler>()
            where TPackageHandlingScheduler : class, IPackageHandlingScheduler<TReceivePackage>;
        /// <summary>
        /// ʹ��Session����
        /// </summary>
        /// <typeparam name="TSessionFactory"></typeparam>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> UseSessionFactory<TSessionFactory>()
            where TSessionFactory : class, ISessionFactory;
        /// <summary>
        /// ʹ��Session
        /// </summary>
        /// <typeparam name="TSession"></typeparam>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> UseSession<TSession>()
            where TSession : IAppSession;
        /// <summary>
        /// ʹ�ð����������ķ�����
        /// </summary>
        /// <returns></returns>
        ISuperSocketHostBuilder<TReceivePackage> UsePackageHandlingContextAccessor();
    }
}