using SuperSocket;

namespace SuperSocket.ProtoBase
{
    public class StringPackageInfo : IKeyedPackageInfo<string>, IStringPackage
    {
        /// <summary>
        /// ��ֵ
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// ��Ϣ��
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string[] Parameters { get; set; }
    }
}