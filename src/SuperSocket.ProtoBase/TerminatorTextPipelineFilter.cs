using System;
using System.Buffers;
using System.Text;

namespace SuperSocket.ProtoBase
{
    public class TerminatorTextPipelineFilter : TerminatorPipelineFilter<TextPackageInfo>
    {
        /// <summary>
        /// �ն��ı��ܵ�ɸѡ��
        /// </summary>
        /// <param name="terminator"></param>
        public TerminatorTextPipelineFilter(ReadOnlyMemory<byte> terminator)
            : base(terminator)
        {

        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="buffer">����</param>
        /// <returns>�ı���Ϣ</returns>
        protected override TextPackageInfo DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            return new TextPackageInfo { Text = buffer.GetString(Encoding.UTF8) };
        }
    }
}
