using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SuperSocket.Server;

namespace SuperSocket.WebSocket.Server
{
    class DelegateSubProtocolHandler : SubProtocolHandlerBase
    {
        private Func<WebSocketSession, WebSocketPackage, ValueTask> _packageHandler;
        /// <summary>
        /// ��ʼ����Э�鴦��ί��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="packageHandler">��������</param>
        public DelegateSubProtocolHandler(string name, Func<WebSocketSession, WebSocketPackage, ValueTask> packageHandler)
            : base(name)
        {
            _packageHandler = packageHandler;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="package">WebSocket��</param>
        /// <returns></returns>
        public override async ValueTask Handle(IAppSession session, WebSocketPackage package)
        {
            await _packageHandler(session as WebSocketSession, package);
        }
    }
}