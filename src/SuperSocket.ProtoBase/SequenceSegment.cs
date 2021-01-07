using System;
using System.Buffers;

namespace SuperSocket.ProtoBase
{
    public class SequenceSegment : ReadOnlySequenceSegment<byte>, IDisposable
    {
        private bool disposedValue;

        private byte[] _pooledBuffer;

        private bool _pooled = false;
        /// <summary>
        /// ��ʼ�����ж�
        /// </summary>
        /// <param name="pooledBuffer">�㼯Buffer</param>
        /// <param name="length">����</param>
        /// <param name="pooled">�Ƿ�㼯</param>
        public SequenceSegment(byte[] pooledBuffer, int length, bool pooled)
        {
            _pooledBuffer = pooledBuffer;
            _pooled = pooled;
            this.Memory = new ArraySegment<byte>(pooledBuffer, 0, length);
        }
        /// <summary>
        /// ��ʼ�����ж�
        /// </summary>
        /// <param name="pooledBuffer">�㼯buffer</param>
        /// <param name="length">����</param>
        public SequenceSegment(byte[] pooledBuffer, int length)
            : this(pooledBuffer, length, true)
        {

        }
        /// <summary>
        /// ��ʼ�����ж�
        /// </summary>
        /// <param name="memory">�ڴ�</param>
        public SequenceSegment(ReadOnlyMemory<byte> memory)
        {
            this.Memory = memory;
        }
        /// <summary>
        /// ��ʼ�����ж�
        /// </summary>
        /// <param name="segment">�ֶ�</param>
        /// <returns></returns>
        public SequenceSegment SetNext(SequenceSegment segment)
        {
            segment.RunningIndex = RunningIndex + Memory.Length;
            Next = segment;
            return segment;
        }
        /// <summary>
        /// ��ʼ�����ж�
        /// </summary>
        /// <param name="memory">�ڴ�</param>
        /// <returns></returns>
        public static SequenceSegment CopyFrom(ReadOnlyMemory<byte> memory)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(memory.Length);
            memory.Span.CopyTo(new Span<byte>(buffer));
            return new SequenceSegment(buffer, memory.Length);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_pooled && _pooledBuffer != null)
                        ArrayPool<byte>.Shared.Return(_pooledBuffer);
                }

                disposedValue = true;
            }
        }
        /// <summary>
        /// GC����
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
