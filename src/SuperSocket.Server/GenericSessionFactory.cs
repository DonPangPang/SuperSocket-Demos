using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using SuperSocket.Channel;

namespace SuperSocket.Server
{
    public class GenericSessionFactory<TSession> : ISessionFactory
        where TSession : IAppSession
    {
        public Type SessionType => typeof(TSession);

        public IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        /// ͨ��Session����
        /// </summary>
        /// <param name="serviceProvider"></param>
        public GenericSessionFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        /// <summary>
        /// ����ͨ��Session����
        /// </summary>
        /// <returns></returns>
        public IAppSession Create()
        {
            return ActivatorUtilities.CreateInstance<TSession>(ServiceProvider);
        }
    }
}