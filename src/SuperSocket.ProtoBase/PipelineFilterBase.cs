using System.Buffers;

namespace SuperSocket.ProtoBase
{
    public abstract class PipelineFilterBase<TPackageInfo> : IPipelineFilter<TPackageInfo>
        where TPackageInfo : class
    {
        public IPipelineFilter<TPackageInfo> NextFilter { get; protected set; }
        
        public IPackageDecoder<TPackageInfo> Decoder { get; set; }

        public object Context { get; set; }

        public abstract TPackageInfo Filter(ref SequenceReader<byte>  reader);
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="buffer">�ַ�����</param>
        /// <returns></returns>
        protected virtual TPackageInfo DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            return Decoder.Decode(ref buffer, Context);
        }
        /// <summary>
        /// ����
        /// </summary>
        public virtual void Reset()
        {
            if (NextFilter != null)
                NextFilter = null;
        }
    }
}