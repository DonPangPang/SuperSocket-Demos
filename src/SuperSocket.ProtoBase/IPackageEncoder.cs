using System.Buffers;

namespace SuperSocket.ProtoBase
{
    public interface IPackageEncoder<in TPackageInfo>
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="writer">�ֽ���д����</param>
        /// <param name="pack">����Ϣ</param>
        /// <returns></returns>
        int Encode(IBufferWriter<byte> writer, TPackageInfo pack);
    }
}