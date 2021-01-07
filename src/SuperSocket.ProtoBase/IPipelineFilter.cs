using System.Buffers;

namespace SuperSocket.ProtoBase
{
    public interface IPipelineFilter
    {
        /// <summary>
        /// ����
        /// </summary>
        void Reset();
        /// <summary>
        /// ������
        /// </summary>
        object Context { get; set; }        
    }

    public interface IPipelineFilter<TPackageInfo> : IPipelineFilter
    {
        /// <summary>
        /// ������
        /// </summary>
        IPackageDecoder<TPackageInfo> Decoder { get; set; }
        /// <summary>
        /// ɸѡ��
        /// </summary>
        /// <param name="reader">�����Ķ���</param>
        /// <returns></returns>
        TPackageInfo Filter(ref SequenceReader<byte> reader);
        /// <summary>
        /// ��һ��ɸѡ
        /// </summary>
        IPipelineFilter<TPackageInfo> NextFilter { get; }
        
    }
}