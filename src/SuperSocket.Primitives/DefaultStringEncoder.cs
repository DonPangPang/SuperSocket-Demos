using System;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;
using SuperSocket.ProtoBase;

namespace SuperSocket
{
    public class DefaultStringEncoder : IPackageEncoder<string>
    {
        private Encoding _encoding;
        /// <summary>
        /// ʵ����Ĭ���ַ�������
        /// </summary>
        public DefaultStringEncoder()
            : this(new UTF8Encoding(false))
        {

        }
        /// <summary>
        /// ʵ����Ĭ���ַ�������
        /// </summary>
        /// <param name="encoding">�����ʽ</param>
        public DefaultStringEncoder(Encoding encoding)
        {
            _encoding = encoding;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="writer">Bufferд����</param>
        /// <param name="pack">�ַ�������</param>
        /// <returns></returns>
        public int Encode(IBufferWriter<byte> writer, string pack)
        {
            return writer.Write(pack, _encoding);
        }
    }
}