using System.Security.Authentication;

namespace SuperSocket
{
    public class ListenOptions
    {
        /// <summary>
        /// IP��ַ
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// �˿�
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// ·��
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// ������־
        /// </summary>
        public int BackLog { get; set; }
        /// <summary>
        /// �Ƿ����ӳ�
        /// </summary>
        public bool NoDelay { get; set; }
        /// <summary>
        /// ��ȫ
        /// </summary>
        public SslProtocols Security { get; set; }
        /// <summary>
        /// ֤��ѡ������
        /// </summary>
        public CertificateOptions CertificateOptions { get; set; }
                
        /// <summary>
        /// תΪ�ַ���
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{nameof(Ip)}={Ip}, {nameof(Port)}={Port}, {nameof(Security)}={Security}, {nameof(Path)}={Path}, {nameof(BackLog)}={BackLog}, {nameof(NoDelay)}={NoDelay}";
        }
    }
}