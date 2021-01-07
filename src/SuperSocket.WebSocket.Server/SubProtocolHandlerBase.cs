using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SuperSocket.Server;

namespace SuperSocket.WebSocket.Server
{
    abstract class SubProtocolHandlerBase : ISubProtocolHandler
    {
        public string Name { get; private set; }
        /// <summary>
        /// ��ʼ����Э�鴦�����
        /// </summary>
        /// <param name="name">����</param>
        public SubProtocolHandlerBase(string name)
        {
            Name = name;
        }        
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="package">WebSocket��</param>
        /// <returns></returns>
        public abstract ValueTask Handle(IAppSession session, WebSocketPackage package);
    }
}