using System.Buffers;
using System.Text;

namespace SuperSocket.ProtoBase
{
    public class LinePipelineFilter : TerminatorPipelineFilter<TextPackageInfo>
    {
        /// <summary>
        /// �����ʽ
        /// </summary>
        protected Encoding Encoding { get; private set; }

        public LinePipelineFilter()
            : this(Encoding.UTF8)
        {

        }
        /// <summary>
        /// �йܵ�ɸѡ��
        /// </summary>
        /// <param name="encoding">�����ʽ</param>
        public LinePipelineFilter(Encoding encoding)
            : base(new[] { (byte)'\r', (byte)'\n' })
        {
            Encoding = encoding;
        }
        /// <summary>
        /// �԰����н���
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        protected override TextPackageInfo DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            return new TextPackageInfo { Text = buffer.GetString(Encoding) };
        }
    }
}
