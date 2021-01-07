using System;
using System.Buffers;

namespace SuperSocket.WebSocket.Extensions
{
    /// <summary>
    /// WebSocket Extensions
    /// https://tools.ietf.org/html/rfc6455#section-9
    /// </summary>
    public interface IWebSocketExtension
    {
        /// <summary>
        /// ����
        /// </summary>
        string Name { get; }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="package"></param>
        void Encode(WebSocketPackage package);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="package"></param>
        void Decode(WebSocketPackage package);
    }
}
