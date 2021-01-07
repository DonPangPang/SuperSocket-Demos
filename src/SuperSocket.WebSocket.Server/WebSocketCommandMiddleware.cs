using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SuperSocket.Channel;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SuperSocket.Server;

namespace SuperSocket.WebSocket.Server
{
    interface IWebSocketCommandMiddleware : IMiddleware
    {

    }

    public class WebSocketCommandMiddleware<TKey, TPackageInfo> : CommandMiddleware<TKey, WebSocketPackage, TPackageInfo>, IWebSocketCommandMiddleware
        where TPackageInfo : class, IKeyedPackageInfo<TKey>
    {
        /// <summary>
        /// ��ʼ��WebSocket�����м��
        /// </summary>
        /// <param name="serviceProvider">�����ṩ</param>
        /// <param name="commandOptions">��������</param>
        public WebSocketCommandMiddleware(IServiceProvider serviceProvider, IOptions<CommandOptions> commandOptions)
            : base(serviceProvider, commandOptions)
        {
            
        }
        /// <summary>
        /// ��ʼ��WebSocket�����м��
        /// </summary>
        /// <param name="serviceProvider">�����ṩ</param>
        /// <param name="commandOptions">��������</param>
        /// <param name="mapper">ӳ����</param>
        public WebSocketCommandMiddleware(IServiceProvider serviceProvider, IOptions<CommandOptions> commandOptions, IPackageMapper<WebSocketPackage, TPackageInfo> mapper)
            : base(serviceProvider, commandOptions, mapper)
        {

        }
    }
}