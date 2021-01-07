using System;

namespace SuperSocket.ProtoBase
{
    public class CommandLinePipelineFilter : TerminatorPipelineFilter<StringPackageInfo>
    {
        /// <summary>
        /// �����йܵ�ɸѡ��
        /// </summary>
        public CommandLinePipelineFilter()
            : base(new[] { (byte)'\r', (byte)'\n' })
        {

        }
    }
}
