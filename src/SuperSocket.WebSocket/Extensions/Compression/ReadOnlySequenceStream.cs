using System;
using System.Buffers;
using System.IO;
using System.IO.Compression;

namespace SuperSocket.WebSocket.Extensions.Compression
{
    class ReadOnlySequenceStream : Stream
    {
        private ReadOnlySequence<byte> _sequence;

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        private long _length;

        public override long Length => _length;

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        /// <summary>
        /// ��ʼ��ֻ������
        /// </summary>
        /// <param name="sequence">����</param>
        public ReadOnlySequenceStream(ReadOnlySequence<byte> sequence)
        {
            _sequence = sequence;
            _length = sequence.Length;
        }
        /// <summary>
        /// ��ϴ�����Ļ�����
        /// </summary>
        public override void Flush()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="buffer">������</param>
        /// <param name="offset">ƫ����</param>
        /// <param name="count">д�����</param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            var firstSpan = _sequence.FirstSpan;
            
            if (firstSpan.IsEmpty)
                return 0;

            var len = Math.Min(firstSpan.Length, count);
            var destSpan = new Span<byte>(buffer, offset, len);

            firstSpan.CopyTo(destSpan);
            _sequence = _sequence.Slice(len);
            return len;
        }
        /// <summary>
        /// �������ĵ�ǰλ������Ϊ����ֵ
        /// </summary>
        /// <param name="offset">�ƶ�����(���ֽ�Ϊ��λ)</param>
        /// <param name="origin">��ʼλ��</param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// ���ó���
        /// </summary>
        /// <param name="value"></param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// д
        /// </summary>
        /// <param name="buffer">������</param>
        /// <param name="offset">ƫ����</param>
        /// <param name="count">д�����</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
