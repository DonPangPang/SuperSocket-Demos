using System.Buffers;

namespace SuperSocket.ProtoBase
{
    public interface IPackageDecoder<out TPackageInfo>
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="buffer">�ֽ���</param>
        /// <param name="context">������</param>
        /// <returns></returns>
        TPackageInfo Decode(ref ReadOnlySequence<byte> buffer, object context);
    }
}