using System.Net;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;

namespace SuperSocket.Client
{
    public class SecurityOptions : SslClientAuthenticationOptions
    {
        /// <summary>
        /// ���ڱ��氲ȫ��֤��ƾ��
        /// </summary>
        public NetworkCredential Credential { get; set; }
    }
}