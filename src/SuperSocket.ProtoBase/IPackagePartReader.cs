using System.Buffers;

namespace SuperSocket.ProtoBase
{
    public interface IPackagePartReader<TPackageInfo>
    {
        /// <summary>
        /// �Ķ�����
        /// </summary>
        /// <param name="package">��</param>
        /// <param name="filterContext">ɸѡ��������</param>
        /// <param name="reader">�Ķ���</param>
        /// <param name="nextPartReader">��һ���Ķ���</param>
        /// <param name="needMoreData">��Ҫ��������</param>
        /// <returns></returns>
        bool Process(TPackageInfo package, object filterContext, ref SequenceReader<byte> reader, out IPackagePartReader<TPackageInfo> nextPartReader, out bool needMoreData);
    }
}